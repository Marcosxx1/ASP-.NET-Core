using IActionResultExemplo.config.exception;
using Lombok.NET;

namespace IActionResultExemplo.exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EntityAlreadyExistsException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message, detail = ex.Detail });
            }
            catch (ResourceNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message, detail = ex.Detail });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno no servidor.");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Erro interno no servidor.",
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}
