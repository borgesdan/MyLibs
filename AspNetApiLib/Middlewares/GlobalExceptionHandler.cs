using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAspNetApiLib.Exceptions;
using System.Net;

namespace MyAspNetApiLib.Middlewares
{
    /// <summary>
    /// Middleware para tratamento de exceções.
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ProblemDetails? problemDetails;
            
            if(exception is HttpStatusCodeException scEx)
                problemDetails = HandleStatusCodeException(scEx.StatusCode, scEx);
            else if (exception is BadHttpRequestException bad && bad.StatusCode != StatusCodes.Status500InternalServerError)
                problemDetails = HandleStatusCodeException(bad.StatusCode, bad);            
            else
                problemDetails = HandleInternalError(exception);

            httpContext.Response.StatusCode = problemDetails.Status!.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            // Retorna 'true' para avisar o ASP.NET que a exceção já foi tratada
            // e ele não precisa tentar tratá-ela de outra forma ou derrubar o app.
            return true;
        }

        private ProblemDetails HandleStatusCodeException(int statusCode, Exception ex)
        {
            _logger.LogWarning("Status code {}: '{message}'.", statusCode, ex.Message);

            return new ProblemDetails
            {
                Status = statusCode,
                Title = "Operação inválida",
                Detail = ex.Message,
            };
        }        

        private ProblemDetails HandleInternalError(Exception ex)
        {
            _logger.LogError(ex, "Status code 500: Foi capturada uma exceção não tratada.");

            //Monta uma resposta padronizada usando o formato ProblemDetails (Padrão RFC 7807)
            return new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erro Interno do Servidor",
                Detail = "Ocorreu um erro interno.",
            };
        }
    }
}
