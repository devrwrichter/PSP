using CSharpFunctionalExtensions;
using FluentValidation.Results;

namespace TransactionService.Help
{
    internal static class ValidationResultExtensions
    {
        internal static IList<string> GetResultErrors(this ValidationResult result)
        {
            var errors = new List<string>();

            foreach (var fail in result.Errors)
                errors.Add(fail.ErrorMessage);

            return errors;

        }
    }
}
