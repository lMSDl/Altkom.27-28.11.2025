using Models;

namespace Services.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProductsAsync();

    }
}
