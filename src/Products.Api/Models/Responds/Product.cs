namespace Products.Api.Models.Responds
{
    public class ProductRespond
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ImgUri { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
