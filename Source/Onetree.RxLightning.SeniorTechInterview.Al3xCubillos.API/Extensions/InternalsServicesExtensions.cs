using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.DataProviders;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.DataProviders.Implementations;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Services;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Services.Implementations;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Extensions
{
    public static class InternalsServicesExtensions
    {
        public static void AddInternals(this IServiceCollection services)
        {
            services.AddDataProviders();
            services.AddServices();
        }

        private static void AddDataProviders(this IServiceCollection services)
        {
            services.AddScoped<IPatientsDataProvider, PatientsDataProvider>();
            services.AddScoped<IUsersDataProvider, UsersDataProvider>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
