using Stone.PSP.Domain.Repositories;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public class CashOutService : ICashOutService
    {
        private readonly IPayableRepository _payableRepository;

        public CashOutService(IPayableRepository payableRepository)
        {
            _payableRepository = payableRepository;
        }
        public async Task<ClientBalanceViewModel> GetBalanceAsync(Guid clientId)
        {
            var result = await _payableRepository.GetBalanceAsync(clientId);
            return new ClientBalanceViewModel { Available = result.Available, WaitingFunds = result.WaitingFunds  };
        }
    }
}
