using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Products.Api.Data.Entities.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
            builder.Property(x => x.ImgUri).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Price).HasPrecision(18, 6).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000);

            builder.HasData(
                new(Guid.NewGuid(), "a", "some", 100, null),
                new(Guid.NewGuid(), "b", "some", 100, null),
                new(Guid.NewGuid(), "c", "some", 100, null),
                new(Guid.NewGuid(), "d", "some", 100, null),
                new(Guid.NewGuid(), "e", "some", 100, null),
                new(Guid.NewGuid(), "f", "some", 100, null),
                new(Guid.NewGuid(), "g", "some", 100, null),
                new(Guid.NewGuid(), "h", "some", 100, null),
                new(Guid.NewGuid(), "i", "some", 100, null),
                new(Guid.NewGuid(), "j", "some", 100, null),
                new(Guid.NewGuid(), "k", "some", 100, null),
                new(Guid.NewGuid(), "l", "some", 100, null),
                new(Guid.NewGuid(), "m", "some", 100, null),
                new(Guid.NewGuid(), "n", "some", 100, null),
                new(Guid.NewGuid(), "o", "some", 100, null),
                new(Guid.NewGuid(), "p", "some", 100, null),
                new(Guid.NewGuid(), "q", "some", 100, null));
        }
    }
}
