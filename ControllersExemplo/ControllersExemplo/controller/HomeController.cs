using ControllersExemplo.model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ControllersExemplo.controller
{
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet("usuario")]
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

        [HttpGet("content-result")]
        public ContentResult ContentResultMetodo()
        {
            return Content("Metodo ContentResult", "text/plain");

        }

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
}
