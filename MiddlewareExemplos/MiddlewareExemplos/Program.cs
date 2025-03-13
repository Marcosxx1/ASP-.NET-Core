internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        
        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("Ola do primeiro middleware! ");
            await next(context);
        });

        app.Use(async(HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync(" | Ola do segundo middleware!! ");
            await next(context);
        });

        app.Use(async(HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync(" | Ola do terceiro middleware!!");
            await next(context);
        });

        app.Run();


    }
}