using FluentAssertions;
using Stone.PSP.Domain.GLPD;

namespace Stone.PSP.Domain.Test
{
    public class GlpdHelpTest
    {
        [Theory]
        [InlineData("371449635398431","8431")]
        [InlineData("3714-4496-3539-8411","8411")]
        [InlineData("3714 4496 3539 8455","8455")]
        public void GetOfuscatedCreditCardNumber_Filled_ShouldBeOk(string creditCardNumber, string digits)
        {
            //Action
            var result = GlpdHelp.GetOfuscatedCreditCardNumber(creditCardNumber);

            //Assert
            result.Should().Be(digits);
        }

        [Fact]
        public void GetOfuscatedCreditCardNumber_ProvideEmptyValue_ShouldBeError()
        {
            //Arrange
            var creditCardNumber = string.Empty;

            //Action
            Action action = () => GlpdHelp.GetOfuscatedCreditCardNumber(creditCardNumber);

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }
    }
}