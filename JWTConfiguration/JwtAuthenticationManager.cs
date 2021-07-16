using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_With_JWT.Models;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace WebAPI_With_JWT.JWTConfiguration
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly JwtConfig jwtConfig;
        public JwtAuthenticationManager(IOptionsMonitor<JwtConfig> _jwtConfig)
        {
            this.jwtConfig = _jwtConfig.CurrentValue;
        }
        private readonly IDictionary<string, string> employees = new Dictionary<string, string>
        { { "swapnil.parate@gmail.com", "password1"}, { "swapnil.parate@outlook.com", "password2"} };

        public string Authenticate(EmployeeCred credentials)
        {
            if (!employees.Any(u => u.Key == credentials.Email && u.Value == credentials.Password))
            { 
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, credentials.Email)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
