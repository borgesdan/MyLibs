using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace MyAspNetApiLib.Extensions
{    
    public class JwtServiceConfiguration
    {
        public string Key { get; set; } = null!;
        public string Issue { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public JwtBearerEvents? Events { get; set; }

        public static JwtServiceConfiguration FromConfiguration(IConfiguration configuration)
        {
            var key = configuration["Jwt:Key"]
                ?? throw new NullReferenceException("Chave Jwt:Key não encontrada nas configurações.");

            var issue = configuration["Jwt:Issuer"]
                ?? throw new NullReferenceException("Chave Jwt:Issuer não encontrada nas configurações.");

            var audience = configuration["Jwt:Audience"]
                ?? throw new NullReferenceException("Chave Jwt:Audience não encontrada nas configurações.");

            return new JwtServiceConfiguration
            {
                Key = key,
                Issue = issue,
                Audience = audience
            };
        }

        public JwtServiceConfiguration WithRequestCookiesEvent(string cookieKey)
        {
            Events ??= new JwtBearerEvents();

            Events.OnMessageReceived = context =>
            {
                // Se o cookie existir, extrai o JWT dele e entrega para o validador do .NET
                if (context.Request.Cookies.ContainsKey(cookieKey))
                {
                    context.Token = context.Request.Cookies[cookieKey];
                }

                return Task.CompletedTask;
            };            

            return this;
        }
    }
}
