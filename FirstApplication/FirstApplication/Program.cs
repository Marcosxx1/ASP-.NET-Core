internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();


        app.Run(async (HttpContext context) => {

            var path = context.Request.Path.ToString().ToLower();

                switch (path)
                {
                    case "/400":
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Erro 400 - Bad Request");
                        break;
                    case "/401":
                        context.Response.StatusCode=401;
                        await context.Response.WriteAsync("Erro 401 - Não autorizado");
                        break;
                    case "/403":
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync("Erro 403 - Proíbido");
                        break;
                    case "/500":
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Erro - 5000 - Internal Server Errror");
                        break;
                }
        });


        app.Run();
    }
}