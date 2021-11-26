﻿namespace Products.Api.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string ImgUri { get; set; } = default!;
        public decimal Price { get; set; }
        public string Description { get; set; } = default!;
    }
}
