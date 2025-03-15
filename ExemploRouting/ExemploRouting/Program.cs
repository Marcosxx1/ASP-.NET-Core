var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Ativa roteamento primeiro
app.UseRouting();

// Middleware customizado, após roteamento
app.UseMiddlewareExample();
app.UseConstraintExample();

// Middleware para logar endpoint (apenas para debug)
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    Endpoint? endpoint = context.GetEndpoint();
    await context.Response.WriteAsync($"Endpoint: {endpoint?.DisplayName}\n");
    await next(context);
});

// Definindo endpoints
app.UseEndpoints(async endpoints =>
{

    endpoints.MapGet("/usuario/int-constraint/{id:int?}", async context =>
    {
        await context.Response.WriteAsync("Chegou no contraint int corretamente");
    });


    endpoints.MapGet("/usuario/{id}", async (context) => // Registrando rota para ser utilizada no middleware "UseMiddlewareExample"
    {
        await context.Response.WriteAsync("GET - Usuário encontrado\n");
    });

    endpoints.MapGet("/usuario/nome/{nomeUsuario}", async (context) => // Registrando rota para ser utilizada no middleware "UseMiddlewareExample
    {
        await context.Response.WriteAsync("GET - Nome do usuário encontrado\n");
    });

    endpoints.MapGet("/get", async (context) =>
    {
        await context.Response.WriteAsync("GET - Endpoint get");
    });

    endpoints.MapPost("/post", async (context) =>
    {
        await context.Response.WriteAsync("POST - Endpoint post");
    });
});

// Middleware final para endpoints inválidos
app.Run(async (context) =>
{
    string path = context.Request.Path;
    await context.Response.WriteAsync($"Endpoint inválido chamado: {path}");
});

app.Run();
