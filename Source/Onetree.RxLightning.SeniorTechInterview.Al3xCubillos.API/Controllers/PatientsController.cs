using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Entities;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Services;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("patients")]
    [Produces("application/json")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientService patientService, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Patient>> Get()
        {
            var patients = await _patientService.GetAllAsync().ConfigureAwait(false);

            return patients;
        }

        [HttpGet("{patientId}")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(string patientId)
        {
            var patient = await _patientService.GetByIdAsync(patientId).ConfigureAwait(false);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
    }
}