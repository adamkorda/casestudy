namespace Products.Api.Dtos
{
    public class ProductsRequestDto
    {
        const int MaxPageSize = 100;
        const int DefaultPageSize = 10;

        private int _pageSize = DefaultPageSize;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = Math.Min(value, MaxPageSize);
            }
        }
    }
}
