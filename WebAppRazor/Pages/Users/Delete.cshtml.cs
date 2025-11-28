using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users
{
    public class DeleteModel : PageModel
    {
        public IService<User> Service { get; }
        public DeleteModel(IService<User> service)
        {
            Service = service;
        }

        public User? SelectedUser { get; set; }

        //zamiast u¿ywaæ parametrów do metod OnGet i OnPost u¿ywamy w³aœciwoœci z atrybutem BindProperty
        //BindProperty dzia³a domyœlnie dla metod POST, ale ustawiaj¹c SupportsGet na true w³¹czamy jego dzia³anie równie¿ dla metod GET
        [BindProperty(SupportsGet = true)]
        public int UserId { get; set; }

        public async Task OnGet()
        {
            SelectedUser = await Service.GetByIdAsync(UserId);
        }

        public async Task<IActionResult> OnPost()
        {
            await Service.DeleteAsync(UserId);
            return RedirectToPage("./Index");
        }


        //poni¿ej wersja z u¿yciem parametrów metod OnGet i OnPost zamiast BindProperty
        /*public async Task OnGet(int userId)
        {
            SelectedUser = await Service.GetByIdAsync(userId);
        }

        public async Task<IActionResult> OnPost(int userId)
        {
            await Service.DeleteAsync(userId);
            return RedirectToPage("./Index");
        }*/
    }
}
