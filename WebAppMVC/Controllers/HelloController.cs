using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace WebAppMVC.Controllers
{
    public class HelloController : Controller
    {
        //parametry są automatycznie mapowane z query string, form data, route data itp.
        //public IActionResult Index(string userInput) // ?userInput=abc
        public IActionResult Index(string? id) // /hello/index/abc       - mapowanie na podstawie route data
        //public IActionResult Index([FromQuery]string? id) // ?id=abc - wymusza mapowanie z query string (inne opcje: FromRoute, FromForm, FromHeader, FromBody)
        {
            //return Content($"Hello from {id}"); // string
           //return Content($"Hello from {id}", "text/html"); // html - niebezpieczne, tylko do testów (umożliwia wstrzyknięcie skryptu)
            return Content(HttpUtility.HtmlEncode($"Hello from {id}"), "text/html"); // html - HtmlEncode zabezpiecza przed wstrzyknięciem skryptu
        }
    }
}
