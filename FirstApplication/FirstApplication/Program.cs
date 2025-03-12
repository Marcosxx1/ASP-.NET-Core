using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();


        app.Run(async (HttpContext context) => {

            var path = context.Request.Path.ToString().ToLower();
            var metodo = context.Request.Method;

            switch (path)
                {

                case "/post-query-string":
                    StreamReader reader = new StreamReader(context.Request.Body);
                    var data = await reader.ReadToEndAsync();

                    Dictionary<string, StringValues> queryDic =
                    QueryHelpers.ParseQuery(data);

                    foreach ( var kvp in queryDic)
                    {
                        var key = kvp.Key;
                        var value = kvp.Value;
                        await context.Response.WriteAsync($"Key: {key}, Value: {value}");
                    }
                break;
                case "/auth":
                    var authorization = context.Request.Headers.Authorization;

                    await context.Response.WriteAsJsonAsync(authorization);
                     break;

                    case "/query-string":
                    if(context.Request.Method == "GET")
                    {
                        if (context.Request.Query.ContainsKey("id"))
                        {

                            string queryParamOne = context.Request.Query["id"];
                            string queryParamOneName = context.Request.Query.GetType().Name;


                            string queryParamTwo = context.Request.Query["name"];
                            string queryParamTwoName = context.Request.Query.GetType().Name;

                            await context.Response.WriteAsync(
                                $"O valor do primeiro QueryParam (id) é: {queryParamOne}\n" +
                                $"O valor do segundo QueryParam (name) é: {queryParamTwo}"
                            );
                        }
                    }
                    break;

                    case "/http-request":

                    await context.Response.WriteAsync($"<p>O caminho sendo utilizado agora e: {path}</p>");
                    await context.Response.WriteAsync($"<p>O metodo sendo utilizado para a rota {path} e: {metodo}</p>");
                    break;
                    case "/headers":
                    // Podemos alterar, adicionar ou remover headers
                    context.Response.Headers["Authorization"] = "eylkd3h9uhf9hf92uhf2o3f2";
                    context.Response.Headers.ContentType = "text/html";

                    await context.Response.WriteAsync("<h1>Um texto em h1 </h1>");

                    break;

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