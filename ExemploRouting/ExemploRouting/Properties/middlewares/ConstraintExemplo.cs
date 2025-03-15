using ExemploRouting.Properties.middlewares;

namespace ExemploRouting.Properties.middlewares
{
    public class ConstraintExemplo
    {

        private RequestDelegate _next;

        public ConstraintExemplo(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await MostraIdUsuario(context);
            await _next(context);
        }

        public async Task MostraIdUsuario(HttpContext context)
        {
            var id = context.Request.Query["id"].ToString();

            if (!string.IsNullOrEmpty(id))
            {
                await context.Response.WriteAsync($"ID usuário: {id}");
            }
            else
            {
                await context.Response.WriteAsync("ID não informado.");
            }
        }

    }
}

public static class ConstraintExampleExtension
{
    public static IApplicationBuilder UseConstraintExample(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ConstraintExemplo>();
    }

}
