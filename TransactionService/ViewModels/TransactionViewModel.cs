using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TransactionService.ViewModels
{
    public record TransactionViewModel : IViewModel
    {
        [JsonIgnore]
        public Guid TransactionId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "A forma de pagamento é obrigatória.")]
        public byte PaymentMethodCode { get; set; }
        public ClientViewModel Client { get; set; }
        public CreditCardViewModel CreditCard { get; set; }
    }
}
