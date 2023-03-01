using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Models;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Services
{
    public interface IUserService
    {
        Task<LoginResponse?> AuthenticateAsync(LoginRequest model);

    }
}
