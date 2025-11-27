using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class ProductsController : Controller
    {

        private readonly Services.Interfaces.IProductsService _productsService;
        public ProductsController(Services.Interfaces.IProductsService productsService)
        {
            _productsService = productsService;
        }


        public async Task<IActionResult> Index()
        {
            var products = await _productsService.GetProductsAsync();

            //przekazanie listy produktów do widoku
            //w widoku użyjemy modelu silnie typowanego
            return View(products);
        }
    }
}
