using Microsoft.AspNetCore.Mvc;
using ModelBindingAndValidations.model;

namespace ModelBindingAndValidations.controller
{
    public class ValidationController : Controller
    {
        [Route("register")]
        public IActionResult Index(Person person)
        {
            return Content($"Perosn: {person}");
        }
    }
}
