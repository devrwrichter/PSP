namespace Stone.PSP.Crosscutting
{
    public record PaginationResponse
    {
        private int _pageNumber;
        public int TableCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get => _pageNumber == 0 ? 1 : _pageNumber; set => _pageNumber = value; }
    }
}