using Microsoft.Extensions.DependencyInjection;
using MyAspNetApiLib.Middlewares;

namespace MyAspNetApiLib.Extensions
{
    public static class ExceptionHandlerServiceCollectionExtensions
    {
        public static IServiceCollection UseGlobalExceptionHandler(this IServiceCollection services)
        {
            //Adiciona o serviço padrão de ProblemDetails
            services.AddProblemDetails();
            //Handler Customizado
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
