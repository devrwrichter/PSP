using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Infra.Mappings
{
    public class PayableMapping : IEntityTypeConfiguration<Payable>
    {
        public void Configure(EntityTypeBuilder<Payable> builder)
        {
            builder.Property(x => x.PaymentDate).IsRequired();
            builder.Property(x => x.TransactionId).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasKey(c => c.Id);
        }
    }
}
