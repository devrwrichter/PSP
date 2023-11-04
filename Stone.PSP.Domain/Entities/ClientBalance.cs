using Microsoft.EntityFrameworkCore;

namespace Stone.PSP.Domain.Entities
{
    [Keyless]
    public record ClientBalance 
    {
        public decimal Available { get; set; }
        public decimal WaitingFunds { get; set; }
    }
}
