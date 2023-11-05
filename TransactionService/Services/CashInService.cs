using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Stone.PSP.Crosscutting;
using Stone.PSP.Domain.DomainObjects;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Services;
using Stone.PSP.Domain.UnitOfWork;
using TransactionService.Help;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public class CashInService : ICashInService
    {
        private IFeeConfigurationCacheService _feeConfigurationCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PspTransaction> _transactionValidator;
        private readonly IMapper _mapper;
        private readonly ILogger<CashInService> _logger;

        public CashInService(ILogger<CashInService> logger, IFeeConfigurationCacheService feeConfigurationCacheService, IUnitOfWork unitOfWork, IMapper mapper, IValidator<PspTransaction> transactionValidator)
        {
            _feeConfigurationCacheService = feeConfigurationCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _transactionValidator = transactionValidator;
            _logger = logger;
        }

        public async Task<TransactionViewModel?> GetTransactionByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.PspTransactionRepository.GetTransactionByIdAsync(id);
                return _mapper.Map<TransactionViewModel>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<IResult<TransactionViewModel>> ProcessTransactionAsync(TransactionViewModel transactionViewModel)
        {
            var pspTransaction = _mapper.Map<PspTransaction>(transactionViewModel);

            var validationResult = await _transactionValidator.ValidateAsync(pspTransaction);

            if (!validationResult.IsValid)
                return new Result<TransactionViewModel>(validationResult);

            //Obtém as regras para criação do Payable
            var feeConfig = _feeConfigurationCacheService.GetConfiguration();

            PayableDomain payableDomain = new(pspTransaction, feeConfig);

            //Obtém o payable com as regras aplicadas
            var payable = payableDomain.GetPayable();

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.PspTransactionRepository.SaveAsync(pspTransaction);
                    await _unitOfWork.PayableRepository.SaveAsync(payable);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex.Message);
                    throw;
                }
            }

            var response = _mapper.Map<TransactionViewModel>(pspTransaction);
            return new Result<TransactionViewModel>(response);
        }

        public async Task<PageResponse<TransactionViewModel>> GetTransactionsWithPaginationAsync(PaginationRequest pagination)
        {
            try
            {
                var transactions = await _unitOfWork.PspTransactionRepository.GetTransactionsAsync(pagination);

                PageResponse<TransactionViewModel> page = new PageResponse<TransactionViewModel>
                {
                    Items = _mapper.Map<ICollection<TransactionViewModel>>(transactions.Items),
                    Pagination = new PaginationResponse
                    {
                        PageNumber = pagination.PageNumber,
                        PageSize = pagination.PageSize,
                        TableCount = transactions.Count
                    }
                };
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ICollection<TransactionViewModel>> GetTransactionsAsync()
        {
            try
            {
                var transactions = await _unitOfWork.PspTransactionRepository.GetTransactionsAsync();
                return _mapper.Map<ICollection<TransactionViewModel>>(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
