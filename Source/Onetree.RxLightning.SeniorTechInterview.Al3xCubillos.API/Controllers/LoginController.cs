using Microsoft.AspNetCore.Mvc;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Models;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Services;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Controllers
{
    [ApiController]
    [Route("login")]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IUserService userService, ILogger<LoginController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var loginResponse = await _userService.AuthenticateAsync(loginRequest).ConfigureAwait(false);

            if (loginResponse == null)
            {
                return Unauthorized(new { message = "Email or password is incorrect" });
            }

            return Ok(loginResponse);
        }
    }
}