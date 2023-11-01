using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Stone.PSP.Infra.Context
{
    public class PaymentContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        { }

    }
}
