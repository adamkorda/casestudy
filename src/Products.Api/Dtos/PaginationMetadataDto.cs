namespace Products.Api.Data.Core
{
    public class PaginationMetadataDto
    {
        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public bool HasPrevious { get; set; }

        public bool HasNext { get; set; }
    }
}