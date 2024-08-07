using Ecommerce_Store.Models;
using Microsoft.EntityFrameworkCore;
namespace Ecommerce_Store.DbContexts
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
   
        }


    }
}
