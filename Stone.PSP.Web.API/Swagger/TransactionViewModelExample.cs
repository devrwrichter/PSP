using Swashbuckle.AspNetCore.Filters;
using TransactionService.ViewModels;

namespace Stone.PSP.Web.API.Swagger
{
    public class TransactionViewModelExample : IExamplesProvider<TransactionViewModel>
    {
        public TransactionViewModel GetExamples()
        {
            return new TransactionViewModel
            {
                Value = 100.50M,
                Description = "Smartband XYZ 3.0",
                PaymentMethodCode = 1,
                Client = new ClientViewModel
                {
                    Id = new Guid("749146d0-3e3c-4241-aef3-ba826ffe5554")
                },
                CreditCard = new CreditCardViewModel
                {
                    CardNumber = "5452 4631 0836 7683",
                    CardHolder = "Portador do cartão de crédito",
                    CardValidateDate = new DateTime(2040, 01, 01),
                    CardVerificationCode = 123
                }
            };
        }
    }
}
