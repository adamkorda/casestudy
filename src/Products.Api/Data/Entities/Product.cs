namespace Products.Api.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUri { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public Product(string name, string imgUri, decimal price, string? description)
        {
            Name = name;
            ImgUri = imgUri;
            Price = price;
            Description = description;
        }

        public Product(int id, string name, string imgUri, decimal price, string? description)
            : this(name, imgUri, price, description)
        {
            Id = id;
        }
    }
}
