namespace Stone.PSP.Domain.Entities
{
    public class Transaction : Entity
    {
        public decimal Value { get; set; }
        public string? Description { get; set; }
        public int PaymentMethodCode { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
