using FluentAssertions;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Enumerators;
using Stone.PSP.Domain.Services;

namespace Stone.PSP.Domain.Test
{
    public class PayableDomainServiceTest
    {
        private readonly IPayableDomainService _domainService = new PayableDomainService();

        [Theory]
        [InlineData(100.00, PaymentMethodCodeType.Credit, 95.00)]
        [InlineData(100.00, PaymentMethodCodeType.Debit, 97.00)]
        [InlineData(100.50, PaymentMethodCodeType.Credit, 95.48)]
        [InlineData(1.00, PaymentMethodCodeType.Credit, 0.95)]
        [InlineData(1.00, PaymentMethodCodeType.Debit, 0.97)]
        public void GetPayable_ShouldBeOk(decimal value, PaymentMethodCodeType code, decimal result)
        {
            //Arrange
            PspTransaction _transaction = new()
            {
                Value = value,
                PaymentMethodCode = (byte)code
            };

            //Action
            var payable = _domainService.GetPayable(_transaction);

            //Assert
            payable.Value.Should().Be(result);
        }
    }
}
