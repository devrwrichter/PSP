using AutoMapper;
using FluentValidation;
using Stone.PSP.Crosscutting;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.GLPD;
using Stone.PSP.Domain.Services;
using Stone.PSP.Domain.UnitOfWork;
using TransactionService.Help;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public class CashInService : ICashInService
    {
        private IPayableDomainService _payableDomainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PspTransaction> _transactionValidator;
        private readonly IMapper _mapper;


        public CashInService(IPayableDomainService payableDomainService, IUnitOfWork unitOfWork, IMapper mapper, IValidator<PspTransaction> transactionValidator)
        {
            _payableDomainService = payableDomainService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _transactionValidator = transactionValidator;
        }

        public async Task<TransactionViewModel?> GetTransactionByIdAsync(Guid id)
        {
            var model = await _unitOfWork.PspTransactionRepository.GetTransactionByIdAsync(id);
            return GetTransactionModelByViewModel(model);
        }

        private TransactionViewModel? GetTransactionModelByViewModel(PspTransaction? model)
        {
            if (model != null)
            {
                return new TransactionViewModel
                {
                    Description = model.Description,
                    PaymentMethodCode = model.PaymentMethodCode,
                    TransactionId = model.Id,
                    Value = model.Value,
                    Client = new ClientViewModel
                    {
                        Id = model.ClientId
                    },
                    CreditCard = new CreditCardViewModel
                    {
                        Holder = model.CardHolder,
                        Number = model.CardNumber,
                        ValidateDate = model.CardExpirationDate,
                        VerificationCode = model.CardVerificationCode
                    }
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<IResult<TransactionViewModel>> ProcessTransactionAsync(TransactionViewModel transactionViewModel)
        {
            var pspTransaction = GetTransactionByViewModel(transactionViewModel);
            var payable = _payableDomainService.GetPayable(pspTransaction);

            var validationResult = await _transactionValidator.ValidateAsync(pspTransaction);

            if (!validationResult.IsValid)
                return new Result<TransactionViewModel>(validationResult);

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.PspTransactionRepository.SaveAsync(pspTransaction);
                    await _unitOfWork.PayableRepository.SaveAsync(payable);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            var response = transactionViewModel with { TransactionId = pspTransaction.Id };
            return new Result<TransactionViewModel>(response);
        }

        private PspTransaction GetTransactionByViewModel(TransactionViewModel transactionViewModel)
        {
            //Uma outra opção seria trabalhar com a Transaction como um objeto de domínio, nesse caso não seria
            //uma classe anêmica e as validações e aplicações de regra de negócio ficariam internas ao objeto.
            //Tudo depende muito da complexidade e das necessidades da empresa e de seus projetos.
            //Exemplo: Stone.PSP.Domain.DomainObjects.
            return new PspTransaction
            {
                ClientId = transactionViewModel.Client.Id,
                PaymentMethodCode = transactionViewModel.PaymentMethodCode,
                Value = transactionViewModel.Value,
                Description = transactionViewModel.Description?.Trim(),
                CardNumber = transactionViewModel.CreditCard.Number.Trim().GetOfuscatedCreditCardNumber(),
                CardHolder = transactionViewModel.CreditCard.Holder.Trim(),
                CardExpirationDate = transactionViewModel.CreditCard.ValidateDate,
                CardVerificationCode = transactionViewModel.CreditCard.VerificationCode
            };
        }

        public async Task<PageResponse<TransactionViewModel>> GetTransactionsWithPaginationAsync(PaginationRequest pagination)
        {
            var transactions = await _unitOfWork.PspTransactionRepository.GetTransactionsAsync(pagination);

            PageResponse<TransactionViewModel> page = new PageResponse<TransactionViewModel> {
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

        public async Task<ICollection<TransactionViewModel>> GetTransactionsAsync()
        {
            var transactions = await _unitOfWork.PspTransactionRepository.GetTransactionsAsync();
            return _mapper.Map<ICollection<TransactionViewModel>>(transactions);
        }    
    }
}
