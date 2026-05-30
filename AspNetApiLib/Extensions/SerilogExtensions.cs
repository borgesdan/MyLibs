using Microsoft.AspNetCore.Builder;
using Serilog;

namespace MyAspNetApiLib.Extensions
{
    public static class SerilogExtensions
    {
        public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
        {
            var currentDir = Environment.CurrentDirectory;
            var logDir = Path.Combine(currentDir, "logs/log.txt");

            // 1. Configurar o Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration) // Lê do appsettings.json
                .Enrich.FromLogContext()
                .WriteTo.Console() // Escreve no console
                .WriteTo.File(logDir, rollingInterval: RollingInterval.Day) // Cria um arquivo novo por dia
                .CreateLogger();

            // 2. Dizer para o Host usar o Serilog em vez do logger padrão
            builder.Host.UseSerilog();

            return builder;
        }

        public static WebApplication UseSerilog(this WebApplication app)
        {
            //Ativa o interceptador na Pipeline para exceções.
            app.UseExceptionHandler();

            app.UseSerilogRequestLogging();
            return app;
        }
    }
}
