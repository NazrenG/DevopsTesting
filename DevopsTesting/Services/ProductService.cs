using DevopsTesting.Repositories;
using Entities;

namespace DevopsTesting.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Product Add(Product product)
        {
          return  _repository.Add(product);

             
        }

        public bool Delete(int id)
        {
return (_repository.Delete(id));    

        }

        public Product? GetProductById(int id)
        {
            return _repository.GetProductById(id);
        }

        public IEnumerable<Product> GetProducts(int top = 0)
        {
            return _repository.GetProducts(top);
        }

        public Product? Update(Product product)
        {
            return (_repository.Update(product));   
        }
    }
}
