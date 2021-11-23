﻿namespace Products.Api.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ImgUri { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
