using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //widok jest szukany w katalogu o nazwie kontrolera (Home) w podkatalogu Views lub w katalogu wspó³dzielonym (Shared)
            return View(); //zwraca widok o nazwie zgodnej z nazw¹ akcji (Index)

            // return View("privacy"); //mo¿emy zwróciæ widok o innej nazwie - w tym przypadku privacy.cshtml
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult Razor()
        {
            return View();
        }
    }
}
