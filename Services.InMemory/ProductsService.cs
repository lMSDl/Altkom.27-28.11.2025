using Models;
using Services.Interfaces;

namespace Services.InMemory
{
    public class ProductsService : IProductsService
    {
        public ProductsService()
        {
            Console.WriteLine("Konstruktor ProductsService");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            await Task.Delay(1000);

            return new List<Product>
            {
                new Product { Name = "Product A", Price = 10.99m },
                new Product { Name = "Product B", Price = 15.49m },
                new Product { Name = "Product C", Price = 7.99m }
            };
        }
    }
}
