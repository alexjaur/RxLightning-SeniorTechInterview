using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Entities;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.DataProviders.Implementations
{
    internal class UsersDataProvider : IUsersDataProvider
    {
        private IEnumerable<User> users = new List<User>()
        { 
            new User() 
            { 
                Id = Guid.NewGuid(), 
                Password = BCrypt.Net.BCrypt.HashPassword("test"),
                Email = "al3x@company.com",
                UserName = "al3x",
                FirstName = "Alex",
                LastName = "Cubillos",
                Roles = new HashSet<Role>()
                {
                    new Role() { Name = "ADMIN" }
                }
            },
        };

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            await Task.Yield();

            return users;
        }
    }
}
