namespace Stone.PSP.Domain.DomainObjects
{
    public class FeeConfiguration
    {
        public int FeeNumber { get; set; }
        public decimal CreditFeePercent { get; set; }
        public decimal DebitFeePercent { get; set; }
        public int Days { get; set; }
        public string Version { get; set; } = "v.1.0";
    }
}
