using Microsoft.Extensions.DependencyInjection;

namespace MyAspNetApiLib.Extensions
{
    public static class CorsServiceCollectionExtensions
    {
        public static IServiceCollection UseCors(this IServiceCollection service, string policyName, params string[] origins)
        {
            service.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    policy =>
                    {
                        policy.WithOrigins(origins)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials(); //Permite que a API receba os Cookies HttpOnly desta origem
                    });
            });

            return service;
        }
    }
}
