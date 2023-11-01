namespace Stone.PSP.Domain.Entities
{
    public record Payable 
    {
        public Guid Id { get; init; }

        public decimal Value { get; init; }
        public int Status { get; init; }
        public DateTime PaymentDate { get; init; }


    }
}
