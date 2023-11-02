using CSharpFunctionalExtensions;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public interface ICashInService
    {
        Task<Result> ProcessTransactionAsync(TransactionViewModel transactionViewModel);
    }
}