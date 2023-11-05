using FluentAssertions;
using Stone.PSP.Domain.DomainObjects;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Enumerators;

namespace Stone.PSP.Domain.Test
{
    public class PayableDomainTest
    {
        [Theory]
        [InlineData(100.00, PaymentMethodCodeType.Credit, 95.00)]
        [InlineData(100.00, PaymentMethodCodeType.Debit, 97.00)]
        [InlineData(100.50, PaymentMethodCodeType.Credit, 95.48)]
        [InlineData(1.00, PaymentMethodCodeType.Credit, 0.95)]
        [InlineData(1.00, PaymentMethodCodeType.Debit, 0.97)]
        public void GetPayable_ShouldBeOk(decimal value, PaymentMethodCodeType code, decimal result)
        {
            //Arrange
            PspTransaction transaction = new()
            {
                Value = value,
                PaymentMethodCode = (byte)code
            };
            var feeConfig = new FeeConfiguration { Days = 30, CreditFeePercent = 0.05M, DebitFeePercent = 0.03M, FeeNumber = 1 };
            PayableDomain domain = new PayableDomain(transaction, feeConfig);

            //Action
            var payable = domain.GetPayable();

            //Assert
            payable.Value.Should().Be(result);
        }
    }
}
