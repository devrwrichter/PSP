namespace Stone.PSP.Domain.Entities
{
    public record Transaction : Entity
    {
        public Guid ClientId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public byte PaymentMethodCode { get; set; }
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public string CardVerificationCode { get; set; }
        public DateTime CardExpirationDate { get; set; }
    }
}
