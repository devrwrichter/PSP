namespace TransactionService.Help
{
    public interface IResult<T>
    {
        public IList<string>? Errors { get; }
        T? Model { get; }
        public bool Success { get; }
    }
}
