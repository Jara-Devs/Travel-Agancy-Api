using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Packages;

public class PackageService : IPackageService
{
    private readonly TravelAgencyContext _context;

    public PackageService(TravelAgencyContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IdResponse>> CreatePackage(PackageRequest request, UserBasic userBasic)
    {
        var responsePermissions = await CheckPermissions(userBasic);
        if (!responsePermissions.Ok) return responsePermissions.ConvertApiResponse<IdResponse>();

        var response = await CheckRequest(request, responsePermissions.Value);
        if (!response.Ok) return response.ConvertApiResponse<IdResponse>();

        var package = response.Value!;

        _context.Packages.Add(package);
        await _context.SaveChangesAsync();

        return new ApiResponse<IdResponse>(new IdResponse { Id = package.Id });
    }

    public async Task<ApiResponse> UpdatePackage(Guid id, PackageRequest request, UserBasic userBasic)
    {
        var responsePermissions = await CheckPermissions(userBasic, id);
        if (!responsePermissions.Ok) return responsePermissions.ConvertApiResponse();

        var package = await _context.Packages.FindAsync(id);
        if (package is null) return new NotFound("Package not found");

        var response = await CheckRequest(request, responsePermissions.Value);
        if (!response.Ok) return response.ConvertApiResponse();

        var newPackage = response.Value!;
        newPackage.Id = package.Id;

        _context.Packages.Update(newPackage);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    public async Task<ApiResponse> RemovePackage(Guid id, UserBasic userBasic)
    {
        var responsePermissions = await CheckPermissions(userBasic, id);
        if (!responsePermissions.Ok) return responsePermissions.ConvertApiResponse();

        var oldPackage = await _context.Packages.FindAsync(id);
        if (oldPackage is null) return new NotFound("Package not found");

        _context.Packages.Remove(oldPackage);
        await _context.SaveChangesAsync();

        return new ApiResponse();
    }

    private async Task<ApiResponse<Guid>> CheckPermissions(UserBasic userBasic, Guid? id = null)
    {
        if (userBasic.Role != Roles.AdminAgency && userBasic.Role != Roles.ManagerAgency)
            return new Unauthorized<Guid>("You don't have permissions");

        var agencyId = await _context.UserAgencies.Where(u => u.Id == userBasic.Id).Select(u => u.AgencyId)
            .FirstOrDefaultAsync();

        if (id is null) return new ApiResponse<Guid>(agencyId);

        var package = await _context.Packages.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (package is null) return new NotFound<Guid>("Package not found");

        return (package.HotelOffers.Any(h => h.AgencyId != agencyId)
            && package.ExcursionOffers.Any(e => e.AgencyId != agencyId
            && package.FlightOffers.Any(f => f.AgencyId != agencyId)))
            ? new Unauthorized<Guid>("You don't have permissions")
            : new ApiResponse<Guid>(agencyId);
    }

    private async Task<ApiResponse<Package>> CheckRequest(PackageRequest request, Guid agencyId, Package? package = null)
    {
        if (request.HotelOffers.Count == 0
            && request.ExcursionOffers.Count == 0
            && request.FlightOffers.Count == 0) 
            return new BadRequest<Package>("You must add at least one offer");

        if (request.Discount is > 100 or < 0)
            return new BadRequest<Package>("Discount must be between 0 and 100");

        var hotelOffers = new List<HotelOffer>();

        foreach (var item in request.HotelOffers)
        {
            var offer = await _context.HotelOffers.FindAsync(item);
            if (offer is null) return new NotFound<Package>("Hotel offer not found");
            if (offer.AgencyId != agencyId) return new Unauthorized<Package>("You don't have permissions");

            if (!Helpers.ValidDate(offer.StartDate)) return new BadRequest<Package>("The offer has expired");

            hotelOffers.Add(offer);
        }

        var excursionOffers = new List<ExcursionOffer>();

        foreach (var item in request.ExcursionOffers)
        {
            var offer = await _context.ExcursionOffers.FindAsync(item);
            if (offer is null) return new NotFound<Package>("Excursion offer not found");
            if (offer.AgencyId != agencyId) return new Unauthorized<Package>("You don't have permissions");

            if (!Helpers.ValidDate(offer.StartDate)) return new BadRequest<Package>("The offer has expired");

            excursionOffers.Add(offer);
        }

        var flightOffers = new List<FlightOffer>();

        foreach (var item in request.FlightOffers)
        {
            var offer = await _context.FlightOffers.FindAsync(item);
            if (offer is null) return new NotFound<Package>("Flight offer not found");
            if (offer.AgencyId != agencyId) return new Unauthorized<Package>("You don't have permissions");

            if (!Helpers.ValidDate(offer.StartDate)) return new BadRequest<Package>("The offer has expired");

            flightOffers.Add(offer);
        }

        package = request.Package(package);
        package.HotelOffers = hotelOffers;
        package.ExcursionOffers = excursionOffers;
        package.FlightOffers = flightOffers;

        return new ApiResponse<Package>(package);
    }
}