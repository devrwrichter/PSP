using FluentValidation.Results;

namespace TransactionService.Help
{
    public class Result<T> : IResult<T>
    {
        public IList<string>? Errors { get; private set; }
        public T? Model { get; }
        public bool Success { get { return Errors != null ? !Errors.Any() : true; } }

        public Result(ValidationResult validationResult)
        {
            Errors = validationResult.GetResultErrors();
        }

        public Result(T? model)
        {
            Model = model;
        }
    }
}
