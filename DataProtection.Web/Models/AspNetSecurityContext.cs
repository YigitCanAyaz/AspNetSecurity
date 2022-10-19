using Microsoft.EntityFrameworkCore;

namespace DataProtection.Web.Models
{
    public class AspNetSecurityContext : DbContext
    {

        public AspNetSecurityContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
