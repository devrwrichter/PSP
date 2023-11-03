using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Infra.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<PspTransaction>
    {
        public void Configure(EntityTypeBuilder<PspTransaction> builder)
        {
            builder.Property(x => x.PaymentMethodCode).IsRequired();
            builder.Property(x => x.CardHolder).IsRequired();
            builder.Property(x => x.CardExpirationDate).IsRequired();
            builder.Property(x => x.CardNumber).IsRequired();
            builder.Property(x => x.CardVerificationCode).IsRequired();
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.Description).IsRequired();

            builder.HasKey(c => c.Id);
            builder.Property(x => x.CreateAt).IsRequired();
        }
    }
}
