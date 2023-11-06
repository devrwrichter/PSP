namespace Stone.PSP.Domain.Entities
{
    public class CreditCard
    {
        public string Number { get; set; }
        public string Holder { get; set; }
        public DateTime ValidateDate { get; set; }
        public int VerificationCode { get; set; }
    }
}
