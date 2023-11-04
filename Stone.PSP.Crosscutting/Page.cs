namespace Stone.PSP.Crosscutting
{
    public class Page<T> where T : class
    {
        public Pagination Pagination { get; set; }
        public IList<T> Items { get; set; }
    }
}
