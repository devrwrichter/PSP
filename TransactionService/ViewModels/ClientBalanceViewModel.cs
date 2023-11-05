namespace TransactionService.ViewModels
{
    public record ClientBalanceViewModel : IViewModel
    {
        public decimal Available { get; set; }
        public decimal WaitingFunds { get; set; }
    }
}
