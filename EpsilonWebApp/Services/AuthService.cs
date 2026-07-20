using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EpsilonWebApp.Services
{
    // Simple demo authentication: a single set of credentials from configuration,
    // shared by both the cookie (Blazor UI) and JWT (REST API) schemes.
    public class AuthService(IConfiguration config)
    {
        public bool ValidateCredentials(string username, string password)
        {
            var expectedUser = config["Auth:Username"];
            var expectedPass = config["Auth:Password"];
            return !string.IsNullOrEmpty(expectedUser)
                && username == expectedUser
                && password == expectedPass;
        }

        public string GenerateJwt(string username)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expireMinutes = int.TryParse(config["Jwt:ExpireMinutes"], out var m) ? m : 60;

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
