using Microsoft.EntityFrameworkCore;

namespace EncodeLabsTest.Models
{
    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        {

        }

        public DbSet<Producto> Productos { get; set; } = null;
    }
}
