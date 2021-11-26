namespace Products.Api.Tests.Helpers.Models
{
    public class PaginationMetadata
    {
        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public bool HasPrevious { get; set; }

        public bool HasNext { get; set; }
    }
}
