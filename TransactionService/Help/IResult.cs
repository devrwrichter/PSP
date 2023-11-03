using FluentValidation.Results;

namespace TransactionService.Help
{
    public interface IResult<T>
    {
        T? Model { get; }
        public bool Success { get; }
    }
}
