using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Transactions;

namespace ModelBindingAndValidations.controller
{
    public class HomeController : Controller
    {

        [Route("book-object")]
        public IActionResult GetBook()
        [Route("from-route/{userId}/{bookId}")]
        public IActionResult FromRouteMethod([FromRoute] int userId, [FromRoute] int bookId)
        {
            return Content($"Value coming from the route using [FromRoute]\nThe user id is: {userId}, The book id is: {bookId}");
        }

        [Route("from-query")]
        public IActionResult FromQueryMethod([FromQuery] int? userId, [FromQuery]int bookId)
        {
            return Content($"Values coming from the query with [FromQuery]\nThe user id is: {userId}, The book id is: {bookId}");

        }


        [Route("bookstore")]
        public IActionResult Index(int? bookid, bool isloggedin)
        {

            if (bookid.HasValue == false)
            {
                return BadRequest("Book id is not supplied or empty");
            }

            if (bookid <= 0)
            {
                return BadRequest("Book cannot be less than zero");
            }

            if(bookid > 1000)
            {
                return BadRequest("Book id cannot be greater than 1000");
            }

            if (isloggedin == false) {
                return Unauthorized();
            }

            return Content($"Book id is {bookid}, and the user is logged in: {isloggedin}");
        }
    }
}
