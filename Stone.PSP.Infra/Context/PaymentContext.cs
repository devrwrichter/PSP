using Microsoft.EntityFrameworkCore;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Infra.Mappings;

namespace Stone.PSP.Infra.Context
{
    public class PaymentContext : DbContext
    {
        public DbSet<PspTransaction> Transactions { get; set; }
        public DbSet<Payable> Payables { get; set; }
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TransactionMapping());
            modelBuilder.ApplyConfiguration(new PayableMapping());
        }

    }
}
