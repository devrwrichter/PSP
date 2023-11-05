using System.Text.Json.Serialization;

namespace TransactionService.ViewModels
{
    public record TransactionViewModel : IViewModel
    {
        [JsonIgnore]
        public Guid TransactionId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public byte PaymentMethodCode { get; set; }
        public ClientViewModel Client { get; set; }
        public CreditCardViewModel CreditCard { get; set; }

        public TransactionViewModel()
        {
            Client = new ClientViewModel();
            CreditCard = new CreditCardViewModel();
        }
    }
}
