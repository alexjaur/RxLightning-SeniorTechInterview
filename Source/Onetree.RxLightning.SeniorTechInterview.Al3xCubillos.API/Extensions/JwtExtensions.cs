using Microsoft.IdentityModel.Tokens;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Entities;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Extensions
{
    public static class JwtExtensions
    {
        private static DateTime GetExpireTime()
        {
            var expireTime = DateTime.UtcNow.Add(Constants.ExpireTime);
            return expireTime;
        }

        private static IEnumerable<Claim> GetClaims(User user)
        {
            var id = user.Id.ToString();

            var claims = new List<Claim>
            {
                new Claim("Id", id),
                new Claim(ClaimTypes.Sid, id),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Expiration, GetExpireTime().ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };

            if (user.Roles != null && user.Roles.Any())
            {
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
            }

            return claims;
        }

        public static string GenerateJwt(this User user, JwtSettings jwtSettings)
        {
            var claims = GetClaims(user);
            var expires = GetExpireTime();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return jwt;
        }
    }
}
