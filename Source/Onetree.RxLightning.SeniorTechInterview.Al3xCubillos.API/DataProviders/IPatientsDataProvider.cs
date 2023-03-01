using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Entities;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.DataProviders
{
    public interface IPatientsDataProvider
    {
        Task<Patient?> GetByIdAsync(string patientId); 
        Task<IEnumerable<Patient>> GetAllAsync();
    }
}
