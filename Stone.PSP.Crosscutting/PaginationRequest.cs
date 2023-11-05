namespace Stone.PSP.Crosscutting
{
    public class PaginationRequest
    {
        private int _pageNumber;        
        public int PageSize { get; set; }
        public int PageNumber { get => _pageNumber == 0 ? 1 : _pageNumber; set => _pageNumber = value; }
    }
}
