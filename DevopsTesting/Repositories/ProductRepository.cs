using Entities;
using Entities.Database;

namespace DevopsTesting.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDb _db;

        public ProductRepository(ProductDb db)
        {
            _db = db;
        }

        public Product Add(Product product)
        {
   
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
           var item= _db.Products.FirstOrDefault(p=>p.ProductID==id);
            if (item == null) return false;
            _db.Products.Remove(item);
            _db.SaveChanges();
            return true;
        }

        public Product? GetProductById(int id)
        {
            var item = _db.Products.FirstOrDefault(p => p.ProductID == id);
            if (item != null) return item;
            return null;
        }

        public IEnumerable<Product> GetProducts(int top = 0)
        {
            return _db.Products.Take(top);
        }

        public Product? Update(Product product)
        {
          _db.Products.Update(product);
            _db.SaveChanges();
            return product;
        }
    }
}
