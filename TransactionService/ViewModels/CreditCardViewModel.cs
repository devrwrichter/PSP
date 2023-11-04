using System.ComponentModel.DataAnnotations;

namespace TransactionService.ViewModels
{
    public class CreditCardViewModel
    {
        [Required(ErrorMessage = "Número do cartão de crédito é necessário.")]
        [CreditCard(ErrorMessage ="Esse número de cartão de crédito é inválido.")] 
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Nome do portador do cartão é necessário.")]
        public string CardHolder { get; set; }

        [Required(ErrorMessage = "Código de verificação do cartão é necessário.")]
        public string CardVerificationCode { get; set; }

        [Required(ErrorMessage = "Data de validade do cartão é necessário.")]
        public DateTime CardValidateDate { get; set; }
    }
}
