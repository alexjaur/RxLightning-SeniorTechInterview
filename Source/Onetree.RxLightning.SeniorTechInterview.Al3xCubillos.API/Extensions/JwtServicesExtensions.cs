using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Models;
using System.Text;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Extensions
{
    public static class JwtServicesExtensions
    { 
        public static void AddJwt(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services
                .AddHttpContextAccessor()
                .AddAuthorization()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                });
        }
    }
}
