using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Entities;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.DataProviders
{
    public interface IUsersDataProvider
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}
