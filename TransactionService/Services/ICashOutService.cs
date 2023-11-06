using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public interface ICashOutService
    {
        Task<ClientBalanceViewModel> GetBalanceAsync(Guid clientId);
    }
}