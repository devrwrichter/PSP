using Microsoft.EntityFrameworkCore;
using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Infra.Context
{
    public class PaymentContext : DbContext
    {
        public DbSet<PspTransaction> Transactions { get; set; }
        public DbSet<Payable> Payables { get; set; }
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        { }

    }
}
