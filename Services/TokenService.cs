using Microsoft.IdentityModel.Tokens;
using movieTickets.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using movieTickets.Settings;

namespace movieTickets.Services
{
   
        internal class TokenService
        {
            internal string GenerateToken(User user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = ApiSettings.GenerateSecretByte();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, user.Username.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
        }
    
}
