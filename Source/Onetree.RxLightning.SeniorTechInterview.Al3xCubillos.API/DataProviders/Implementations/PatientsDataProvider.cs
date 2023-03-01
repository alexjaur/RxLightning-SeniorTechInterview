using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Entities;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.DataProviders.Implementations
{
    internal class PatientsDataProvider : IPatientsDataProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public PatientsDataProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            using var httpClient = GetHttpClient();
            var patients = await httpClient.GetFromJsonAsync<Patient[]>("patients");

            return patients ?? Enumerable.Empty<Patient>();
        }

        public async Task<Patient?> GetByIdAsync(string patientId)
        {
            Patient? patient = null;

            if (!string.IsNullOrWhiteSpace(patientId))
            {
                using var httpClient = GetHttpClient();
                var response = await httpClient.GetAsync($"patient/{patientId}");


                if (response.IsSuccessStatusCode)
                {
                    patient = await response.Content.ReadFromJsonAsync<Patient>();
                }
            }

            return patient;
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.PatientsHttpClientName);

            return httpClient;
        }
    }
}
