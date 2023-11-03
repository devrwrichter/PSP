namespace TransactionService.ViewModels
{
    public record TransactionViewModel
    {
        public decimal Value { get; set; }
        public string? Description { get; set; }
        public int PaymentMethodCode { get; set; }
        public ClientViewModel Client { get; set; }
        public CreditCardViewModel CreditCard { get; set; }       
    }
}
