using System.Security.Claims;

namespace MyAspNetApiLib.Security
{
    /// <summary>
    /// Representa o retorno do serviço de geração de token JWT.
    /// </summary>
    /// <param name="Token">O token JWT gerado.</param>
    public record JwtServiceResult(string Token);

    public record JwtServiceRequest(string Key, string Issuer, string Audience, int Expires);

    public interface IJwtService
    {
        JwtServiceResult GenerateJwtToken(List<Claim> claims, JwtServiceRequest request);
    }
}
