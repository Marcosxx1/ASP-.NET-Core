using Lombok.NET;
using Microsoft.AspNetCore.Http;
using MiddlewareExemplos.Middleware;
using System.Threading.Tasks;

namespace MiddlewareExemplos.Middleware
{
    [AllArgsConstructor]
    public partial class HelloCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Query.ContainsKey("primeiroNome") &&
                context.Request.Query.ContainsKey("sobreNome"))
            {
                var nomeCompleto = $"{context.Request.Query["primeiroNome"]} {context.Request.Query["sobreNome"]}";
                await context.Response.WriteAsync(nomeCompleto);
            }
            else
            {
                await context.Response.WriteAsync("Parâmetros 'primeiroNome' e 'sobreNome' são necessários.");
            }

            await _next(context);
        }
    }
}

public static class HelloCustomMiddlewareExtension
{
    public static IApplicationBuilder UseHelloCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HelloCustomMiddleware>();
    }
}