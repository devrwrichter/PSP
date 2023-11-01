using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Stone.PSP.Domain.Test")]
namespace Stone.PSP.Domain.GLPD
{
    internal static class GlpdHelp
    {
        private const int VisibleDigitsLength = 4;

        /// <summary>
        /// Para utilização desse método é um pré-requisito a validação do Credit Card.
        /// </summary>
        /// <param name="creditCardNumber"></param>
        /// <returns>string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static string GetOfuscatedCreditCardNumber(this string creditCardNumber)
        {
            if (string.IsNullOrEmpty(creditCardNumber))
            {
                throw new ArgumentNullException("Empty credit card.");
            }

            var lastFourDigits = creditCardNumber.Substring(creditCardNumber.Length - VisibleDigitsLength, VisibleDigitsLength);
            return lastFourDigits;
        }
    }
}
