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
