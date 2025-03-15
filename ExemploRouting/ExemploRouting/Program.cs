var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    Endpoint? endpoint = context.GetEndpoint();
    await context.Response.WriteAsync($"Endpoint: {endpoint?.DisplayName}\n");
    await next(context);
});

// Ativa roteamento
app.UseRouting();

// Criando endpoints
app.UseEndpoints(endpoints =>
{
    app.Use(async (HttpContext context, RequestDelegate next) =>
    {
        Endpoint? endpoint = context.GetEndpoint();
        await context.Response.WriteAsync($"\nEndpoint: {endpoint?.DisplayName}");
        await next(context);
    });

    endpoints.MapGet("get", async (context) =>
    {
        await context.Response.WriteAsync("GET - Endpoint get");
    });

    endpoints.MapPost("post", async (context) =>
    {
        await context.Response.WriteAsync("POST - Endpoint post");
    });

});

app.Run(async (context) =>
{
    string path = context.Request.Path;
    await context.Response.WriteAsync($"Endpoint inválido chamado: {path}");
});

app.Run();
