using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Infra.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.Property(x => x.PaymentMethodCode).IsRequired();
            builder.Property(x => x.CardHolder).HasColumnType("varchar(255)").IsRequired();
            builder.Property(x => x.CardExpirationDate).HasColumnType("date").IsRequired();
            builder.Property(x => x.CardNumber).HasColumnType("varchar(4)").IsRequired();
            builder.Property(x => x.CardVerificationCode).HasColumnType("varchar(4)").IsRequired();
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(100)").IsRequired();

            builder.HasKey(c => c.Id);
            builder.Property(x => x.CreateAt).HasColumnType("datetime").IsRequired();
        }
    }
}
