using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.InMemory;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users
{
    public class IndexModel : PageModel
    {
        public IService<User> Service { get; }
        public IndexModel(IService<User> service)
        {
            Service = service;
        }
    
        public IEnumerable<User> Users { get; set; }
    
        public async Task OnGet(string? input)
        {
            var users = await Service.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(input))
            {
                var properties = typeof(User).GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                users = users.Where(u => properties.Any(x => x.GetValue(u)?.ToString()?
                    .Contains(input, StringComparison.OrdinalIgnoreCase) == true)).ToArray();
            }
            Users = users;
        }
    }
}
