using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.GLPD;

namespace Stone.PSP.Domain.DomainObjects
{
    /// <summary>
    /// Classe criada apenas para exemplificação.
    /// </summary>
    public class TransactionDomain
    {
        public Guid ClientId { get; private set; }
        public decimal Value { get; private set; }
        public string? Description { get; private set; }
        public byte PaymentMethodCode { get; private set; }
        public string? CardNumber { get; private set; }
        public string? CardHolder { get; private set; }
        public string? CardVerificationCode { get; private set; }
        public DateTime CardExpirationDate { get; private set; }
        public TransactionDomain(PspTransaction transaction)
        {            
            ClientId = transaction.ClientId;
            Value = transaction.Value;
            Description = transaction.Description;
            PaymentMethodCode = transaction.PaymentMethodCode;
            CardNumber = transaction.CardNumber?.GetOfuscatedCreditCardNumber();
            CardHolder = transaction.CardHolder;
            CardVerificationCode = transaction.CardVerificationCode;                
            CardExpirationDate = transaction.CardExpirationDate;
        }

        public void ApplyBusinessRules()
        {
            //Por exemplo aplica regras de negócio.
        }

        public bool IsValid()
        {
            //Chamando o fluentvalidation aqui dentro;
            //Exemplo
            return false;
        }
    }
}
