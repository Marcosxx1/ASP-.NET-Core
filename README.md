[Controllers em ASP.NET](#controllers-em-asp-net)
[ContentResult](#contentresult)
[JsonResult e Serialização pelo ASP.NET Core](#jsonresult-e-serializacao-pelo-aspnet-core)



## Controllers em ASP.NET

No ASP.NET, **Controllers** são classes responsáveis por lidar com requisições HTTP (como GET, POST, etc.) e retornar respostas. Eles fazem parte do padrão **MVC (Model-View-Controller)**, onde o *Controller* atua como intermediário entre a lógica da aplicação e a interface do usuário.

### Como tudo funciona?

1. **`Program.cs`**  
   Este arquivo configura a aplicação para reconhecer e utilizar controllers automaticamente:
   
   - `builder.Services.AddControllers()` registra os controllers como serviços, permitindo que o ASP.NET os descubra de forma automática.
   - `app.MapControllers()` mapeia os endpoints definidos nos controllers para as rotas correspondentes da aplicação.

2. **`HomeController.cs`**  
   Define um controller com um endpoint simples. Essa classe geralmente herda de `ControllerBase` ou `Controller`, e cada método público (com atributos como `[HttpGet]`, `[HttpPost]`, etc.) define uma rota que pode ser acessada por requisições HTTP.

3. Porém a classe ControllersExemplo não terá acesso aos métodos como
`HttpContext`
`Request`, `Response`
`ModelBinding` e `ModelState`
`ActionResult`, `IActionResult`

Esses recursos só vêm ao herdar de ControllerBase ou Controller, que fazem parte da infraestrutura do ASP.NET.
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
3. Porém a classe ControllersExemplo não terá acesso aos métodos como
`HttpContext`
`Request`, `Response`
`ModelBinding` e `ModelState`
`ActionResult`, `IActionResult`

Esses recursos só vêm ao herdar de ControllerBase ou Controller, que fazem parte da infraestrutura do ASP.NET.

Para conseguirmos utilizar estes métodos devemos utilizar a seguinte estrutura (que no momento está básica para fins didáticos...)

```csharp
using Microsoft.AspNetCore.Mvc;

namespace ControllersExemplo.controller
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        
        [HttpGet("usuario")] // É possível utilizar mais que uma definição de rotas
        [HttpGet("/")]
        public string Usuario()
        {
            
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            return $"Método usuário. IP de origem: {ip}";
        }

        [HttpGet("contato")]
        public string Contato()
        {
            return "Método contato.";
        }

        [HttpGet("info")]
        public string Info()
        {
            return "Método Info.";
        }
    }
}
```

## ContentResult

É um tipo de resposta, baseada em um MIME type especificado.
MIME Types representados pelo tipo do conteúdo como `text/plain` `text/html` `application/json` e por ai vai, 

Request -> Controller -> Action -> return Content Result > response body com o conteúdo

Assim como no exemplo abaixo:

```csharp
using Microsoft.AspNetCore.Mvc;

namespace ControllersExemplo.controller
{
    [ApiController]
     public class HomeController : ControllerBase
    {
        [HttpGet("content-result")]
        public ContentResult ContentResultMetodo()
        {
            return new ContentResult()
            {
                Content = "Método ContentResult",
                ContentType = "text/plain"
            };
        }

        // Ou resumidamente assim:
        [HttpGet("content-result")]
        public ContentResult ContentResultMetodoSimplificado()
        {
            return Content("Metodo ContentResult","text/plain");

        }
    }
}
```

## JsonResult e Serialização pelo ASP.NET Core

O ASP.NET Core oferece suporte nativo à serialização automática de objetos para JSON, facilitando o desenvolvimento de APIs REST.

---

### Serialização automática

Sempre que você retorna um objeto de um método de controller decorado com `[ApiController]`, o ASP.NET Core converte esse objeto para JSON automaticamente usando o `System.Text.Json`.

### Exemplo de controller

```csharp
[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet("json-result")]
    public JsonResult JsonResultMetodo()
    {
        Person person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Marcos",
            LastName = "Doe",
            Age = 19
        };
        return new JsonResult(person);
    }

    [HttpGet("serializado-pelo-asp")]
    public Person SerializadoPeloAsp()
    {
        return new Person
        {
            Id = Guid.NewGuid(),
            Name = "Marcos",
            LastName = "Doe",
            Age = 19
        };
    }
}
```

---

### Alternativas

- `return new JsonResult(objeto);` — forma explícita, mas mais verbosa.
- `return objeto;` — forma mais comum e idiomática.
- `return Ok(objeto);` — também recomendado, adiciona controle sobre o status HTTP (200 OK).

---

### Comparativo com Spring Boot

| ASP.NET Core                      | Spring Boot             |
|----------------------------------|--------------------------|
| `return objeto;`                 | `return objeto;`         |
| Usa `System.Text.Json`           | Usa `Jackson`            |
| `[ApiController]` + `ControllerBase` | `@RestController`     |
| JSON automático na resposta      | JSON automático na resposta |

---