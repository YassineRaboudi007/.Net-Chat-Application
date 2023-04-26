using ChatApplication.Domain.Abstractions.Auth;
using ChatApplication.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApplication.Infrastructure.Jwt
{
    public class JwtProvider : IJwtProvider
    {


        public string Generate(User user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email.ToString()),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("Secret Key")
                ),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "Its Me",
                "Users",
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                null
            );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public Guid GetIdFromToken(string token)
        {
            if (token == null)
            {
                throw new Exception("Didnt Porvide User Token");
            }
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return Guid.Parse( jwt.Claims.First(c => c.Type == "sub").Value);
        }
    }
}
