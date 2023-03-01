using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Entities;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Services
{
    public interface IPatientService
    {
        Task<Patient?> GetByIdAsync(string id);
        Task<IEnumerable<Patient>> GetAllAsync();
    }
}
