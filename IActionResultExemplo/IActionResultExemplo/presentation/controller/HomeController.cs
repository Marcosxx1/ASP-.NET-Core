using Microsoft.AspNetCore.Mvc;

namespace IActionResultExemplo.presentation.controller
{
    public class HomeController : Controller
    {
        [Route("book")]
        public IActionResult Index()
        {
            if (!Request.Query.ContainsKey("bookid"))
            {
             //   return Content("Book id is not supplied");
             // return new BadRequestResult();
             return BadRequest("Book id is not supplied");
            }
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                return BadRequest("Book id cannot be empty");

             }

            // Id do Livro deve ser entre 1 e 1000
            int? bookid = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);

            if (bookid < 0 )
            {
                return BadRequest("Book id can't be less then or equal to zero");

             }

            if (bookid > 1000) {
                return BadRequest("Book id cannot be greater than 1000");

             }

            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                return Unauthorized("Unauthorized.");
            }

            return File("/exemplo.pdf", "application/pdf");
        }
    }
}
