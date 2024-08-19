using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_DataBase.Core;
using Travel_Agency_DataBase.Queries;
using Travel_Agency_DataBase.Queries.Reserves;
using Travel_Agency_DataBase.Queries.Users;
using Travel_Agency_Domain;
using Travel_Agency_Domain.Images;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Payments;
using Travel_Agency_Domain.Reactions;
using Travel_Agency_Domain.Services;
using Travel_Agency_Domain.Users;
using Travel_Agency_Logic;
using Travel_Agency_Logic.Auth;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Images;
using Travel_Agency_Logic.Offers;
using Travel_Agency_Logic.Packages;
using Travel_Agency_Logic.Reactions;
using Travel_Agency_Logic.Reserves;
using Travel_Agency_Logic.Services;

namespace Travel_Agency_Api;

public static class ProgramServices
{
    public static void AddAllServices(this IServiceCollection services)
    {
        // Configure queries
        services.AddScoped<IQueryEntity<UserAgency>, UserAgencyQuery>();
        services.AddScoped<IQueryEntity<User>, UserAppQuery>();

        services.AddScoped<IQueryEntity<Flight>, PublicQuery<Flight>>();
        services.AddScoped<IQueryEntity<Excursion>, PublicQuery<Excursion>>();
        services.AddScoped<IQueryEntity<Hotel>, PublicQuery<Hotel>>();
        services.AddScoped<IQueryEntity<TouristActivity>, PublicQuery<TouristActivity>>();
        services.AddScoped<IQueryEntity<Facility>, PublicQuery<Facility>>();
        services.AddScoped<IQueryEntity<TouristPlace>, PublicQuery<TouristPlace>>();
        services.AddScoped<IQueryEntity<City>, PublicQuery<City>>();

        services.AddScoped<IQueryEntity<ExcursionOffer>, PublicQuery<ExcursionOffer>>();
        services.AddScoped<IQueryEntity<HotelOffer>, PublicQuery<HotelOffer>>();
        services.AddScoped<IQueryEntity<FlightOffer>, PublicQuery<FlightOffer>>();

        services.AddScoped<IQueryEntity<Package>, PublicQuery<Package>>();

        services.AddScoped<IQueryEntity<ReserveTicket>, ReserveQuery<ReserveTicket>>();
        services.AddScoped<IQueryEntity<ReserveTourist>, ReserveQuery<ReserveTourist>>();

        services.AddScoped<IQueryEntity<Reaction>, PublicQuery<Reaction>>();

        // Configure commands
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<IExcursionService, ExcursionService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<ITouristPlaceService, TouristPlaceService>();
        services.AddScoped<IFlightService, FlightService>();
        services.AddScoped<ITouristActivityService, TouristActivityService>();
        services.AddScoped<IFacilityService, FacilityService>();
        services.AddScoped<ICityService, CityService>();

        services.AddScoped<IOfferService<HotelOffer>, OfferService<HotelOffer>>();
        services.AddScoped<IOfferService<ExcursionOffer>, OfferService<ExcursionOffer>>();
        services.AddScoped<IOfferService<FlightOffer>, OfferService<FlightOffer>>();

        services.AddScoped<IPackageService, PackageService>();

        services.AddScoped<IReserveService<ReserveTicket, PaymentTicket>, ReserveTicketService>();
        services.AddScoped<IReserveService<ReserveTourist, PaymentOnline>, ReserveTouristService>();

        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IReactionService, ReactionService>();

        services.AddScoped<SecurityService>();
    }

    public static void AddDataBase(this IServiceCollection services, IConfigurationRoot configuration)
    {
        // Obtener la cadena de conexión desde la configuración
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configurar el contexto de la base de datos para usar PostgreSQL
        services.AddDbContext<TravelAgencyContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public static void AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });
    }

    public static void ConfigureOdata(this IServiceCollection services)
    {
        services.AddControllers().AddOData(opt =>
            opt.Select().Count().Filter().Expand().Select().OrderBy().SetMaxTop(50)
                .AddRouteComponents("odata", GetEdmModel()));
    }

    public static void ConfigurePolicies(this IServiceCollection service)
    {
        service.AddAuthorization(options =>
            options.AddPolicy(Policies.App, policy => policy.RequireRole(Roles.AdminApp, Roles.EmployeeApp)));
        service.AddAuthorization(options =>
            options.AddPolicy(Policies.Agency,
                policy => policy.RequireRole(Roles.AdminAgency, Roles.ManagerAgency, Roles.EmployeeAgency)));
        service.AddAuthorization(options =>
            options.AddPolicy(Policies.AgencyManager,
                policy => policy.RequireRole(Roles.AdminAgency, Roles.ManagerAgency)));
        service.AddAuthorization(options =>
            options.AddPolicy(Policies.AgencyEmployee,
                policy => policy.RequireRole(Roles.AdminAgency, Roles.ManagerAgency, Roles.EmployeeAgency)));
        service.AddAuthorization(options =>
            options.AddPolicy(Policies.ReserveTouristAgency,
                policy => policy.RequireRole(Roles.AdminAgency, Roles.ManagerAgency, Roles.Tourist)));
    }

    private static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        // Configure entities
        builder.EntitySet<UserAgency>("UserAgency");
        builder.EntitySet<User>("UserApp");

        builder.EntitySet<Flight>("Flight");
        builder.EntitySet<Excursion>("Excursion");
        builder.EntitySet<Hotel>("Hotel");
        builder.EntitySet<TouristPlace>("TouristPlace");
        builder.EntitySet<TouristActivity>("TouristActivity");
        builder.EntitySet<Facility>("Facility");
        builder.EntitySet<City>("City");

        builder.EntitySet<HotelOffer>("HotelOffer");
        builder.EntitySet<ExcursionOffer>("ExcursionOffer");
        builder.EntitySet<FlightOffer>("FlightOffer");

        builder.EntitySet<Package>("Package");

        builder.EntitySet<ReserveTicket>("ReserveTicket");
        builder.EntitySet<ReserveTourist>("ReserveTourist");

        builder.EntitySet<Image>("Image");
        builder.EntitySet<Reaction>("Reaction");

        return builder.GetEdmModel();
    }
}