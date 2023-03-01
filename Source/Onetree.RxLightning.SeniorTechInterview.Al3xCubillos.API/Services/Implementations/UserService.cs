using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.DataProviders;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Extensions;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Models;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Services.Implementations
{
    internal class UserService : IUserService
    {
        private readonly IUsersDataProvider _usersDataProvider;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<UserService> _logger;


        public UserService(IUsersDataProvider usersDataProvider, JwtSettings jwtSettings, ILogger<UserService> logger)
        {
            _usersDataProvider = usersDataProvider;
            _jwtSettings = jwtSettings;
            _logger = logger;
        }


        public async Task<LoginResponse?> AuthenticateAsync(LoginRequest loginRequest)
        {
            LoginResponse? loginResponse = null;

            var users = await _usersDataProvider.GetAllAsync();

            var user = users.FirstOrDefault(u =>
                loginRequest.Email.Equals(u.Email)
                && BCrypt.Net.BCrypt.Verify(loginRequest.Password, u.Password) 
            );

            if (user != null)
            {
                try
                {
                    var token = user.GenerateJwt(_jwtSettings);

                    loginResponse = new LoginResponse()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        Fullname = $"{user.FirstName} {user.LastName}",
                        AccessToken = token
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error when trying to generate the JWT");
                }
            }

            return loginResponse;
        }
    }
}
