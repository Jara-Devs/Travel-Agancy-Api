using Microsoft.EntityFrameworkCore;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Services
{
    public class OverNightExcursionExcursionService : IOverNightExcursionService
    {
        private readonly TravelAgencyContext _context;

        public OverNightExcursionExcursionService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IdResponse>> CreateOverNightExcursion(OverNightExcursionRequest excursion,
            UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized<IdResponse>("You don't have permissions");

            if (await _context.Excursions.AnyAsync(a => a.Name == excursion.Name))
                return new NotFound<IdResponse>("The excursion with same name already exists");

            if (!await _context.Hotels.AnyAsync(h => h.Id == excursion.HotelId))
                return new NotFound<IdResponse>("The referenced hotel does not exist");

            if (excursion.Places.Count == 0 || excursion.Activities.Count == 0)
                return new BadRequest<IdResponse>("There must be at least one tourist activity and one tourist place");

            var response = await CreateExcursion(excursion);
            if (!response.Ok) return response.ConvertApiResponse<IdResponse>();

            var entity = response.Value!;
            _context.Excursions.Add(entity);
            await _context.SaveChangesAsync();

            return new ApiResponse<IdResponse>(new IdResponse { Id = entity.Id });
        }

        public async Task<ApiResponse> UpdateOverNightExcursion(int id, OverNightExcursionRequest excursion,
            UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            if (!await _context.Excursions.AnyAsync(e => e.Id == id))
                return new NotFound("Excursion not found");

            if (await _context.Excursions.AnyAsync(a => a.Name == excursion.Name && a.Id != id))
                return new NotFound("The excursion with same name already exists");

            if (!await _context.Hotels.AnyAsync(h => h.Id == excursion.HotelId))
                return new NotFound("The referenced hotel does not exist");

            if (excursion.Places.Count == 0 || excursion.Activities.Count == 0)
                return new BadRequest("There must be at least one tourist activity and one tourist place");

            var response = await CreateExcursion(excursion);
            if (!response.Ok) return response.ConvertApiResponse();

            var newExcursion = response.Value!;
            newExcursion.Id = id;

            _context.Update(newExcursion);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        public async Task<ApiResponse> DeleteOverNightExcursion(int id, UserBasic user)
        {
            if (!CheckPermissions(user))
                return new Unauthorized("You don't have permissions");

            var excursion = await _context.Excursions.FindAsync(id);
            if (excursion is null) return new NotFound("Excursion not found");

            _context.Excursions.Remove(excursion);
            await _context.SaveChangesAsync();

            return new ApiResponse();
        }

        private static bool CheckPermissions(UserBasic user) =>
            user.Role == Roles.AdminApp || user.Role == Roles.EmployeeApp;

        private async Task<ApiResponse<Excursion>> CreateExcursion(ExcursionRequest request)
        {
            var excursion = request.Excursion();
            foreach (var item in request.Places)
            {
                var place = await this._context.TouristPlaces.FindAsync(item);
                if (place is null)
                    return new BadRequest<Excursion>("Not found tourist place");

                excursion.Places.Add(place);
            }

            foreach (var item in request.Activities)
            {
                var activity = await this._context.TouristActivities.FindAsync(item);
                if (activity is null)
                    return new BadRequest<Excursion>("Not found tourist activity");

                excursion.Activities.Add(activity);
            }

            return new ApiResponse<Excursion>(excursion);
        }
    }
}