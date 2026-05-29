using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyAspNetApiLib.Security
{
    public class JwtService : IJwtService
    {
        public JwtServiceResult GenerateJwtToken(List<Claim> claims, JwtServiceRequest request)
        {
            ThrowIfInvalidRequest(request);

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(request.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(request.Expires),
                Issuer = request.Issuer,
                Audience = request.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);

            return new JwtServiceResult(result);
        }

        private static void ThrowIfInvalidRequest(JwtServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Key)
                || string.IsNullOrWhiteSpace(request.Issuer)
                || string.IsNullOrWhiteSpace(request.Audience))
            {
                throw new InvalidOperationException("A requisição para criação de um token Jwt contém dados nulos ou vazios.");
            }
        }
    }
}
