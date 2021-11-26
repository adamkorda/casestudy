
using Microsoft.EntityFrameworkCore;

using Products.Api.Data.Entities;
using Products.Api.Data.Entities.Configurations;

namespace Products.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfiguration(new ProductConfiguration());

        public DbSet<Product> Products { get; set; } = default!;
    }
}
