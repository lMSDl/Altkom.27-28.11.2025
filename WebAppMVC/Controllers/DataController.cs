using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index(string? name, int? age)
        {
            //przekazanie danych do widoku za pomocą ViewData i ViewBag
            //ViewData jest słownikiem klucz-wartość, a ViewBag jest dynamicznym obiektem
            ViewData["name"] = name ?? "Guest";
            ViewBag.Age = age.HasValue ? age.ToString() : "unknown";


            /*if (TempData["message"] == null)
                TempData["message"] = "TempData from Index";*/


            //instrukcja zachowująca dane w TempData po odczytaniu ich w widoku
            TempData.Keep("TempDataKeep");
            return View();
        }

        public IActionResult Index2(string? name, int? age)
        {
            ViewData["name"] = name ?? "Guest";
            ViewBag.Age = age.HasValue ? age.ToString() : "unknown";
            /*if (TempData["message"] == null)
                TempData["message"] = "TempData from Index";*/

            return View("index");
        }


        public IActionResult Redirect(string? name, int? age)
        {
            ViewData["name"] = name ?? "Guest";
            ViewBag.Age = age.HasValue ? age.ToString() : "unknown";

            //przekazanie danych do widoku za pomocą TempData
            //TempData przechowuje dane przez jedną redirekcję
            TempData["message"] = "TempData from Redirect";

            return RedirectToAction("Index");
        }


        public IActionResult TempDataKeep()
        {
            TempData["TempDataKeep"] = "TempData from TempDataKeep";

            return RedirectToAction(nameof(TempDataStep));
        }

        public IActionResult TempDataStep()
        {
            //odczytanie danych z TempData bez usuwania ich
            var peeked = TempData.Peek("TempDataKeep");


            return RedirectToAction("Index");
        }
    }
}
