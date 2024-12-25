using Microsoft.EntityFrameworkCore;

namespace Entities.Database
{
    public class ProductDb:DbContext
    {
        public ProductDb(DbContextOptions<ProductDb> dbContext):base(dbContext)
        {
            
        }
        public virtual DbSet<Product> Products { get; set; }
    }
}
