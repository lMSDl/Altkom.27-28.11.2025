using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebAppMVC.Controllers
{
    //włączenie walidacji AntiForgeryToken dla całego kontrolera (dla wszystkich metod POST)
    [AutoValidateAntiforgeryToken]
    public class UsersController : Controller
    {

        private readonly IService<User> _service;
        public UsersController(IService<User> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _service.GetAllAsync();
            return View(users);
        }

        //włączenie walidacji AntiFOrgeryToken dla pojedynczej metody
        //[AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Search(string? input)
        {
            var users  = await _service.GetAllAsync();
            if(!string.IsNullOrWhiteSpace(input))
            {
                var properties = typeof(User).GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                users = users.Where(u => properties.Any(x => x.GetValue(u)?.ToString()?
                    .Contains(input, StringComparison.OrdinalIgnoreCase) == true)).ToArray();
            }

            return View("Index", users);

        }
    }
}
