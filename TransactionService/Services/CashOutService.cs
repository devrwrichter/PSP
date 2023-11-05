using Microsoft.Extensions.Logging;
using Stone.PSP.Domain.Repositories;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public class CashOutService : ICashOutService
    {
        private readonly IPayableRepository _payableRepository;
        private readonly ILogger<CashOutService> _logger;

        public CashOutService(IPayableRepository payableRepository, ILogger<CashOutService> logger)
        {
            _payableRepository = payableRepository;
            _logger = logger;
        }
        public async Task<ClientBalanceViewModel> GetBalanceAsync(Guid clientId)
        {
            try
            {
                var result = await _payableRepository.GetBalanceAsync(clientId);
                if (result != null)
                    return new ClientBalanceViewModel { Available = result.Available, WaitingFunds = result.WaitingFunds };
                else
                    return null;
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
