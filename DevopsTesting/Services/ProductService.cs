using Entities;

namespace DevopsTesting.Services
{
    public class ProductService : IProductService
    {
        private static List<Product> products = new List<Product>
        {
            new Product
            {
                İd= 1,
                Name="Acer",
                Price=3200
            },
              new Product
            {
                İd= 2,
                Name="Apple",
                Price=4800
            },
        };
        public Product Add(Product product)
        {
            var lastNumber = products.Count > 0 ? products.Max(x => x.İd) + 1 : 1;
            product.İd = lastNumber;
            products.Add(product);

            return product;
        }

        public bool Delete(int id)
        {
            var item = products.FirstOrDefault(p => p.Equals(id));
            if (item != null) { return products.Remove(item); }
            return false;
        }

        public Product? GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.Equals(id));
        }

        public IEnumerable<Product> GetProducts(int top = 0)
        {
            return top == 0 ? products : products.Take(top);
        }

        public Product? Update(Product product)
        {
            var item = products.FirstOrDefault(p => p.Equals(product));
            if (item != null)
            {
                item.Name = product.Name;
                item.Price = product.Price;
                return item;
            }
            return null;
        }
    }
}
