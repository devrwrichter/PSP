namespace Stone.PSP.Crosscutting
{
    public class PageResponse<T> where T : class
    {
        public PaginationResponse Pagination { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
