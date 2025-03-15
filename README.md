("Controllers em ASP.NET")[#controllers-em-asp-net]

## Controllers em ASP.NET

No ASP.NET, **Controllers** são classes responsáveis por lidar com requisições HTTP (como GET, POST, etc.) e retornar respostas. Eles fazem parte do padrão **MVC (Model-View-Controller)**, onde o *Controller* atua como intermediário entre a lógica da aplicação e a interface do usuário.

### Como tudo funciona?

1. **`Program.cs`**  
   Este arquivo configura a aplicação para reconhecer e utilizar controllers automaticamente:
   
   - `builder.Services.AddControllers()` registra os controllers como serviços, permitindo que o ASP.NET os descubra de forma automática.
   - `app.MapControllers()` mapeia os endpoints definidos nos controllers para as rotas correspondentes da aplicação.

2. **`HomeController.cs`**  
   Define um controller com um endpoint simples. Essa classe geralmente herda de `ControllerBase` ou `Controller`, e cada método público (com atributos como `[HttpGet]`, `[HttpPost]`, etc.) define uma rota que pode ser acessada por requisições HTTP.

---

## Código

### Program.cs

```csharp
namespace ControllersExemplo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(); // 1 - Deixa o framework encontrar e adicionar todos os controllers

            var app = builder.Build();

            app.MapControllers(); // 2 - Mapeia os endpoints dos controllers
            
            app.Run();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ControllersExemplo.controller
{
    public class HomeController
    {

        [Route("usuario")]
        public string metodoUm()
        {
            return "Olá do método um!";
        }
    }


}

```

