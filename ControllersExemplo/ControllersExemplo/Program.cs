namespace ControllersExemplo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(); // 1 - Deixamos o framework encontrar todos os controllers e adicioná-los sozinho

            var app = builder.Build();

            app.MapControllers(); // 2 - Mapeamos os endpoints dos controllers. O passo 3 é criar o controller de fato em HomeController.cs
            
            app.Run();
        }
    }
}
