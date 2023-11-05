using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Enumerators;
using System.ComponentModel;

namespace Stone.PSP.Domain.DomainObjects
{
    public class PayableDomain
    {
        private FeeConfiguration _feeConfig;
        private PspTransaction _transaction;

        public PayableDomain(PspTransaction transaction, FeeConfiguration feeConfig)
        {
            _transaction = transaction;
            _feeConfig = feeConfig;
        }

        /// <summary>
        /// Obtém o Payable com o state desejado (regras aplicadas)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public Payable GetPayable()
        {
            switch ((PaymentMethodCodeType)_transaction.PaymentMethodCode)
            {
                case PaymentMethodCodeType.Debit:
                    return GetPayableToDebitPayment();
                case PaymentMethodCodeType.Credit:
                    return GetPayableToCreditPayment();
                default:
                    throw new InvalidEnumArgumentException("Tipo de payment method não implementado");
            }
        }

        private Payable GetPayableToCreditPayment()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                TransactionId = _transaction.Id,
                Value = Math.Round(_transaction.Value - (_transaction.Value * _feeConfig.CreditFeePercent), 2),
                Status = (int)PayableStatusType.WaitingFunds,
                PaymentDate = DateTime.Today.AddDays(_feeConfig.Days)
            };
        }

        private Payable GetPayableToDebitPayment()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                TransactionId = _transaction.Id,
                Value = Math.Round(_transaction.Value - (_transaction.Value * _feeConfig.DebitFeePercent), 2),
                Status = (int)PayableStatusType.Paid,
                PaymentDate = DateTime.Today
            };
        }
    }
}
