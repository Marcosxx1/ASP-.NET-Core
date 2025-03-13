
using MiddlewareExemplos.middleware;

namespace MiddlewareExemplos.middleware
{
    public class Middlewares : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync(" | Ola de dentro do Middleware customizado! 1 ");
            await next(context);
            await context.Response.WriteAsync(" | Ola de dentro do Middleware customizado! 2 ");

        }
    }
}

public static class CustomMiddlewareExemplo
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {

        return app.UseMiddleware<Middlewares>();
    }

}