using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.DataProviders;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Entities;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Extensions;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Services.Implementations
{
    internal class PatientService : IPatientService
    {
        private readonly IPatientsDataProvider _patientsDataProvider;


        public PatientService(IPatientsDataProvider patientsDataProvider)
        {
            _patientsDataProvider = patientsDataProvider;
        }


        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            var patients = await _patientsDataProvider.GetAllAsync();

            var patientsFormatted = patients.Select(patient =>
            {
                // encode 'PatientId'
                patient.PatientId = patient.PatientId.Encode();

                return patient;
            });

            return patientsFormatted;
        }

        public async Task<Patient?> GetByIdAsync(string id)
        {
            // decode 'PatientId'
            var patientId = id.Decode();

            var patient = await _patientsDataProvider.GetByIdAsync(patientId);

            if (patient != null)
            {
                // set encoded 'PatientId'
                patient.PatientId = id;
            }

            return patient;
        }
    }
}
