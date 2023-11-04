using FluentAssertions;
using FluentValidation;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Enumerators;
using Stone.PSP.Domain.Validators;

namespace Stone.PSP.Domain.Test
{
    public class TransactionValidatorTest
    {
        private readonly IValidator<PspTransaction> _validator = new TransactionValidator();
        private PspTransaction _transaction = new()
        {
            Value = 500.0M,
            CardExpirationDate = DateTime.Today.AddYears(5),
            CardHolder = "Nome do portador do cartão",
            CardVerificationCode = "724",
            CardNumber = "5452 4631 0836 7683",
            ClientId = Guid.NewGuid(),
            Description = "Smartband XYZ 3.0",
            PaymentMethodCode = (byte)PaymentMethodCodeType.Credit
        };

        [Fact]
        public void Validate_ValueZero_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { Value = 0.0M };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorValueIsMinorOrEqualThanZero));
        }

        [Fact]
        public void Validate_CardExpirationDate_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { CardExpirationDate = DateTime.Today.AddDays(-5) };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorCardValidateDate));
        }

        [Fact]
        public void Validate_CardHolder_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { CardHolder = string.Empty };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorCardHolderEmpty));
        }

        [Fact]
        public void Validate_CardVerificationCodeMinor_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { CardVerificationCode = "1" };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorCardVerificationCode));
        }

        [Fact]
        public void Validate_CardVerificationCodeMajor_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { CardVerificationCode = "55555" };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorCardVerificationCode));
        }

        [Fact]
        public void Validate_CardNumberEmpty_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { CardNumber = string.Empty };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorCardNumberEmpty));
        }

        [Fact]
        public void Validate_ClientIdEmpty_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { ClientId = Guid.Empty };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorInvalidClientId));
        }

        [Fact]
        public void Validate_DescriptionEmpty_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { ClientId = Guid.Empty };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorInvalidClientId));
        }

        [Fact]
        public void Validate_DescriptionInvalidLengthMin_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { Description = "d".PadRight(TransactionValidator.MinLengthDescription-1) };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorInvalidDescriptionLength));
        }

        [Fact]
        public void Validate_DescriptionInvalidLengthMax_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { Description = "d".PadRight(TransactionValidator.MaxLengthDescription + 1) };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorInvalidDescriptionLength));
        }

        [Fact]
        public void Validate_PaymentCodeInvalidValue_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = _transaction with { PaymentMethodCode = (byte)3 };

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().Satisfy(x => x.ErrorMessage.Equals(TransactionValidator.ErrorPaymentMethodCode));
        }

        [Fact]
        public void Validate_AllErrors_ShouldBeError()
        {
            //Arrange
            PspTransaction transactionTest = new();

            //Action
            var result = _validator.Validate(transactionTest);

            //Assert
            result.Errors.Should().HaveCount(8);
        }

        [Fact]
        public void Validate_ValidObject_ShouldBeOk()
        {
            //Action
            var result = _validator.Validate(_transaction);

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
