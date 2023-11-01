namespace Stone.PSP.Domain.Entities
{
    public record PspTransaction : Entity
    {
        public Guid ClientId { get; set; }
        public decimal Value { get; set; }
        public string? Description { get; set; }
        public int PaymentMethodCode { get; set; }
        public string? CardNumber { get; set; }
        public string? CardHolder { get; set; }
        public int CardVerificationCode { get; set; }
        public DateTime CardExpirationDate { get; set; }
    }
}
