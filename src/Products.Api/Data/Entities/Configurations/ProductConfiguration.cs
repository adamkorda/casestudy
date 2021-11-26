using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Products.Api.Data.Entities.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
            builder.Property(x => x.ImgUri).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Price).HasPrecision(18, 6).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000);
        }
    }
}
