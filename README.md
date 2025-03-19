[Controllers em ASP.NET](#controllers-em-asp-net)
[ContentResult](#contentresult)
[JsonResult e Serialização pelo ASP.NET Core](#jsonresult-e-serializacao-pelo-aspnet-core)
[Decoradores de Vinculação de Parâmetros no ASP.NET Core](#decoradores-de-vinculacao-de-parametros-no-asp-net-core)




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

Segue uma nova sessão explicando os decoradores:

---

# Decoradores de Vinculação de Parâmetros no ASP.NET Core

No ASP.NET Core, os decoradores **[FromRoute]**, **[FromBody]**, **[FromQuery]**, **[FromHeader]** e **[FromForm]** são usados para indicar de onde os dados dos parâmetros de uma ação devem ser extraídos. A seguir, veja cada um deles com pequenos snippets de código.

---

## Quadro Explicativo

| Decorador     | Descrição                                                                                          | Exemplo de Uso                                                                                                             |
|---------------|------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| **[FromRoute]** | Vincula o valor do parâmetro à rota definida.                                                   | `GET /api/products/5` <br> `public IActionResult GetProduct([FromRoute] int id)`                                            |
| **[FromQuery]** | Extrai o valor de um parâmetro da string de consulta da URL.                                     | `GET /api/products?category=books` <br> `public IActionResult GetProducts([FromQuery] string category)`                      |
| **[FromBody]**  | Lê o corpo da requisição, útil para dados enviados via JSON ou XML.                             | `POST /api/products` <br> `public IActionResult CreateProduct([FromBody] Product product)`                                  |
| **[FromHeader]**| Obtém o valor de um cabeçalho HTTP da requisição.                                               | `public IActionResult CheckAuth([FromHeader(Name = "Authorization")] string token)`                                        |
| **[FromForm]**  | Extrai dados enviados por formulários, geralmente via POST com `enctype="multipart/form-data"`.  | `public IActionResult UploadImage([FromForm] IFormFile image)`                                                            |

---
 
 ## Decoradores de Vinculação de Parâmetros no ASP.NET Core

No ASP.NET Core, os decoradores **[FromRoute]**, **[FromBody]**, **[FromQuery]**, **[FromHeader]** e **[FromForm]** indicam de onde os dados dos parâmetros devem ser obtidos.

### [FromRoute]
Utilizado para vincular dados da rota ao parâmetro da ação.

**Exemplo:**
```csharp
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    // Exemplo: GET /api/products/5
    [HttpGet("{id}")]
    public IActionResult GetProduct([FromRoute] int id)
    {
        // Lógica para obter o produto pelo id
        return Ok();
    }
}
```

## [FromQuery]
Extrai dados da string de consulta (query string) da URL.

**Exemplo:**
```csharp
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    // Exemplo: GET /api/products?category=books
    [HttpGet]
    public IActionResult GetProducts([FromQuery] string category)
    {
        // Lógica para filtrar produtos por categoria
        return Ok();
    }
}
```

## [FromBody]
Utilizado para vincular o corpo da requisição ao parâmetro. Muito usado com dados em JSON.

**Exemplo:**
```csharp
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    // Exemplo: POST /api/products
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        // Lógica para criar um novo produto
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }
}
```

## [FromHeader]
Extrai dados de um cabeçalho HTTP específico.

**Exemplo:**
```csharp
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    // Exemplo: Requisição com cabeçalho Authorization
    [HttpGet("check")]
    public IActionResult CheckAuth([FromHeader(Name = "Authorization")] string token)
    {
        // Lógica para verificar o token de autorização
        return Ok();
    }
}
```

## [FromForm]
Utilizado para vincular dados enviados via formulários (tipicamente em POST).

**Exemplo:**
```csharp
[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    // Exemplo: POST /api/upload com enctype="multipart/form-data"
    [HttpPost]
    public IActionResult UploadImage([FromForm] IFormFile image)
    {
        // Lógica para processar o arquivo enviado
        return Ok();
    }
}
```

Claro! Aqui está a sessão explicando **Model Binding** e **ModelState** no ASP.NET Core, com exemplos e explicações simples. Tudo estruturado no formato de um arquivo `.md`:

---

# Model Binding e ModelState no ASP.NET Core

## O que é Model Binding?

**Model Binding** é o processo automático pelo qual o ASP.NET Core converte os dados da requisição HTTP (query string, route, headers, body, forms, etc.) em objetos .NET.

Por exemplo, ao receber um JSON via `POST`, o ASP.NET Core pode automaticamente popular uma classe C# com os dados enviados, sem a necessidade de fazer parsing manual.

### Exemplo de Model Binding com um modelo:

```csharp
public class UserDto
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

```csharp
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser([FromBody] UserDto user)
    {
        // O ASP.NET automaticamente faz o binding do JSON para o objeto UserDto
        return Ok(user);
    }
}
```

---

## Fontes suportadas pelo Model Binding

O binding pode vir de diferentes fontes:

| Fonte         | Atributo         | Exemplo de método                          |
|---------------|------------------|--------------------------------------------|
| Query string  | `[FromQuery]`     | `GetUser([FromQuery] string name)`         |
| Rota          | `[FromRoute]`     | `GetUser([FromRoute] int id)`              |
| Corpo (JSON)  | `[FromBody]`      | `CreateUser([FromBody] UserDto user)`      |
| Formulário    | `[FromForm]`      | `UploadFile([FromForm] IFormFile file)`    |
| Cabeçalho     | `[FromHeader]`    | `CheckToken([FromHeader] string token)`    |

---

## O que é ModelState?

**ModelState** é um dicionário que armazena o estado da validação dos dados recebidos pelo Model Binding. Ele é automaticamente populado com erros se os dados não forem válidos (por exemplo, campos obrigatórios ausentes, tipos incompatíveis, etc.).

> Quando usamos `[ApiController]`, a verificação do `ModelState` é feita automaticamente! Se o modelo não for válido, o ASP.NET Core retorna **400 Bad Request** com os detalhes dos erros.

`isValid` - Especifica caso exista ao menos um erro de validação ou não (true ou false)
`Values` - Contem cada valor de propriedade do modelo corespondente À propriedade "Errors"  que contem a lista de erros de validação daquele modelo
`ErrorCount` - Retorna o número de erros 
---

### Exemplo com validação de modelo:

```csharp
public class RegisterDto
{
    [Required]
    public string Email { get; set; }

    [Range(18, 100)]
    public int Age { get; set; }
}
```

```csharp
[ApiController]
[Route("api/register")]
public class RegisterController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            // Isso raramente será necessário com [ApiController], mas é possível
            return BadRequest(ModelState);
        }

        return Ok("Registro válido.");
    }
}
```

---

## Quando podemos usar ModelState manualmente?

Mesmo com `[ApiController]`, pode haver situações onde queremos verificar o `ModelState` de forma explícita, como em validações condicionais ou quando não está usando `[ApiController]`.

---

## Resumo

- **Model Binding**: converte dados da requisição em objetos .NET.
- **ModelState**: armazena o resultado da validação do modelo.
- `[ApiController]`: cuida automaticamente da validação e resposta 400 em caso de erro.
