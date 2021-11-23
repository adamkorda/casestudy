namespace Products.Api.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImgUri { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public Product(Guid id, string name, string imgUri, decimal price, string? description)
        {
            Id = id;
            Name = name;
            ImgUri = imgUri;
            Price = price;
            Description = description;
        }
    }
}
