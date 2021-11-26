using Products.Api.Data.Entities;

namespace Products.Api.Data.Seeder.Utilities
{
    public static class Utils
    {
        public static IEnumerable<Product> GenerateProducts(int count)
        {
            var random = new Random();
            for (var i = 1; i <= count; i++)
            {
                yield return new($"ProductName{i}",
                                 $"ProductImage{i}",
                                 random.Next(0, 10000),
                                 i % 2 == 0 ? $"ProductDescription{i}" : null);
            }
        }
    }
}
