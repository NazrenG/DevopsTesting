using Entities;

namespace DevopsTesting.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(int top = 0);
        Product? GetProductById(int id);
        Product Add(Product product);
        Product? Update(Product product);
        bool Delete(int id);
    }
}
