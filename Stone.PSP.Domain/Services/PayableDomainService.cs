using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Enumerators;
using System.ComponentModel;

namespace Stone.PSP.Domain.Services
{
    public class PayableDomainService : IPayableDomainService
    {
        //Seria ideal que esses dados ficassem armazenados fora da aplicação, para facilitar a manutenção,
        //apenas colocando aqui devido ser teste e sem ter tempo.
        private const decimal CreditFee = 0.05M;
        private const decimal DebitFee = 0.03M;
        private const int DaysParameterPaymentDateCredit = 30;

        public Payable GetPayable(PspTransaction transaction)
        {
            //Se esse processamento fosse mais complexo poderia ser usado uma Strategy aqui, mas é muito simples.
            var method = (PaymentMethodCodeType)transaction.PaymentMethodCode;

            switch (method)
            {
                case PaymentMethodCodeType.Debit:
                    return GetPayableToDebitPayment(transaction);
                case PaymentMethodCodeType.Credit:
                    return GetPayableToCreditPayment(transaction);
                default:
                    throw new InvalidEnumArgumentException("Tipo de payment method não implementado");
            }
        }

        private Payable GetPayableToCreditPayment(PspTransaction transaction)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                TransactionId = transaction.Id,
                Value = Math.Round(transaction.Value - (transaction.Value * CreditFee),2),
                Status = (int)PayableStatusType.WaitingFunds,
                PaymentDate = DateTime.Today.AddDays(DaysParameterPaymentDateCredit)
            };
        }

        private Payable GetPayableToDebitPayment(PspTransaction transaction)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                TransactionId = transaction.Id,
                Value = Math.Round(transaction.Value - (transaction.Value * DebitFee),2),
                Status = (int)PayableStatusType.Paid,
                PaymentDate = DateTime.Today
            };
        }
    }
}
