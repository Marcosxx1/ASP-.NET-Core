using Lombok.NET;
using MiddlewareExemplos.middleware;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddTransient<Middlewares>();
        var app = builder.Build();
        
        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("Ola do primeiro middleware! ");
            await next(context);
        });

        //app.UseCustomMiddleware();
        app.UseHelloCustomMiddleware();

        app.Use(async(HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync(" | Ola do terceiro middleware!!");
            await next(context);
        });

        app.UseWhen(
            context => context.Request.Query.ContainsKey("nome"),
            app => {
                app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Hello from middleware branch");
                    await next();
                });
            });

        app.Run();


    }
}