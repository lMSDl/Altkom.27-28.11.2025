using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Interfaces;
using System.Threading.Tasks;

namespace WebAppRazor.Pages.Users.AddOrEdit
{
    public class AddOrEditModel : PageModel
    {
        public IService<User> Service { get; }
        public AddOrEditModel(IService<User> service)
        {
            Service = service;
        }

        [BindProperty]
        public User? SelectedUser { get; set; }

        public async Task OnGet(int id)
        {
            SelectedUser = await Service.GetByIdAsync(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (SelectedUser.Id == 0)
            {
                await Service.Create(SelectedUser);
            }
            else
            {
                await Service.UpdateAsync(id, SelectedUser);
            }
            return RedirectToPage("../Index");
        }
    }
}
