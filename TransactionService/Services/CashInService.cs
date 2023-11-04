﻿using FluentValidation;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.GLPD;
using Stone.PSP.Domain.Services;
using Stone.PSP.Domain.UnitOfWork;
using System.ComponentModel.Design;
using TransactionService.Help;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public class CashInService : ICashInService
    {
        private IPayableDomainService _payableDomainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PspTransaction> _transactionValidator;

        public CashInService(IUnitOfWork unitOfWork, IPayableDomainService payableDomainService, IValidator<PspTransaction> transactionValidator)
        {
            _unitOfWork = unitOfWork;
            _payableDomainService = payableDomainService;
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
                        CardHolder = model.CardHolder,
                        CardNumber = model.CardNumber,
                        CardValidateDate = model.CardExpirationDate,
                        CardVerificationCode = model.CardVerificationCode
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
            return new PspTransaction
            {
                ClientId = transactionViewModel.Client.Id,
                PaymentMethodCode = transactionViewModel.PaymentMethodCode,
                Value = transactionViewModel.Value,
                Description = transactionViewModel.Description?.Trim(),
                CardNumber = transactionViewModel.CreditCard.CardNumber.Trim().GetOfuscatedCreditCardNumber(),//TODO
                CardHolder = transactionViewModel.CreditCard.CardHolder.Trim(),
                CardExpirationDate = transactionViewModel.CreditCard.CardValidateDate,
                CardVerificationCode = transactionViewModel.CreditCard.CardVerificationCode
            };
        }        
    }
}
