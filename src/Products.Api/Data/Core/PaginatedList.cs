namespace Products.Api.Data.Core
{
    public class PaginatedList<T> : List<T>
    {
        public PaginationMetadata Metadata { get; set; }

        public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
            : base(items)
        {
            Metadata = new()
            {
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
    }
}