using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyAspNetApiLib.Extensions
{
    public static class JwtServiceCollectionExtensions
    {
        /// <summary>
        /// Adiciona uma configuração para uso de Jwt bearer token. Os dados são obtidos do appsettings.json através do IConfiguration.
        /// </summary>
        public static IServiceCollection UseJwtConfiguration(this IServiceCollection services, JwtServiceConfiguration configuration)
        {
            var key = !string.IsNullOrWhiteSpace(configuration.Key) ? configuration.Key : throw new InvalidOperationException("O parâmetro Key está nulo ou vazio.");
            var issue = !string.IsNullOrWhiteSpace(configuration.Issue) ? configuration.Issue : throw new InvalidOperationException("O parâmetro Issue está nulo ou vazio.");
            var audience = !string.IsNullOrWhiteSpace(configuration.Audience) ? configuration.Audience : throw new InvalidOperationException("O parâmetro Audience está nulo ou vazio.");

            var simetricKey = Encoding.ASCII.GetBytes(key);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(simetricKey),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issue,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero,
                };

                if(configuration.Events != null)
                    options.Events = configuration.Events;
            });

            return services;
        }
    }
}
