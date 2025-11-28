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

        //włączenie walidacji AntiForgeryToken dla pojedynczej metody
        //[AutoValidateAntiforgeryToken]
        //wyłączenie walidacji AntiForgeryToken dla pojedynczej metody
        //[IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Search(string? input)
        {
            var users = await _service.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(input))
            {
                var properties = typeof(User).GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                users = users.Where(u => properties.Any(x => x.GetValue(u)?.ToString()?
                    .Contains(input, StringComparison.OrdinalIgnoreCase) == true)).ToArray();
            }

            return View("Index", users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult Add()
        {
            return View("Edit", new User());
        }

        [HttpPost]
        //public async Task<IActionResult> AddOrEditUser(int id, string name, string password) //przykład bez użycia atrybutów do mapowania danych z formularza
        //public async Task<IActionResult> AddOrEditUser(int id, string name, [FromForm(Name = "password")] string pass) //przykład użycia atrybutu FromForm do mapowania nazwy pola formularza na inny parametr metody
        //public async Task<IActionResult> AddOrEditUser(User user) //przykład mapowania całego modelu
        public async Task<IActionResult> AddOrEditUser(int id, [Bind("Name", "Password")] User user) //przykład użycia atrybutu Bind do ograniczenia mapowania tylko do wybranych właściwości modelu
        {
            if (id == 0)
            {
                await _service.Create(user);
            }
            else
            {
                await _service.UpdateAsync(user.Id, user);
            }
            return RedirectToAction("Index");
        }

    }
}
