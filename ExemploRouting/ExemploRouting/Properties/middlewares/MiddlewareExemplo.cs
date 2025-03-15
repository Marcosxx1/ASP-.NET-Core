using ExemploRouting.Properties.middlewares;

namespace ExemploRouting.Properties.middlewares
{
    
public class MiddlewareExemplo
    {
        private RequestDelegate next;

        public MiddlewareExemplo(RequestDelegate next)
        {
            this.next = next;
        }

        public async  Task Invoke(HttpContext context)
        {
            // Pegar os valores do request params => "/usuario/{id} e /usuario/nome/{nomeUsuario}

            string? idUsuario = RetornaNomeUsuarioOuStringVazia(context.Request.RouteValues["id"], context);
            string nomeUsuario = RetornaValorOuStringVazia(context.Request.RouteValues["nomeUsuario"], context);
            MostraNomeEIdUsuarioNaResponse(idUsuario, nomeUsuario, context);

            await next(context);
        }

        private async void MostraNomeEIdUsuarioNaResponse(string idUsuario, string nomeUsuario, HttpContext context)
        {
            await context.Response.WriteAsync($"ID usuário: {idUsuario}\nNome do usuário: {nomeUsuario}");
        }

        public string RetornaNomeUsuarioOuStringVazia(object requestValue, HttpContext context)
        {
                return Convert.ToString(context.Request.RouteValues["id"]) ?? string.Empty;
        }

        public string RetornaValorOuStringVazia(object requestValue, HttpContext context)
        {
            return requestValue?.ToString() ?? string.Empty;
        }

    }
}

public static class MiddlewareExemploExtensions
{
    public static IApplicationBuilder UseMiddlewareExample(this IApplicationBuilder app)
    {
        return app.UseMiddleware<MiddlewareExemplo>();
    }
}
