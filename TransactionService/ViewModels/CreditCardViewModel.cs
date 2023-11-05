using System.ComponentModel.DataAnnotations;

namespace TransactionService.ViewModels
{
    public class CreditCardViewModel : IViewModel
    {
        [Required(ErrorMessage = "Número do cartão de crédito é necessário.")]
        [CreditCard(ErrorMessage ="Esse número de cartão de crédito é inválido.")] 
        public string Number { get; set; }

        [Required(ErrorMessage = "Nome do portador do cartão é necessário.")]
        public string Holder { get; set; }

        [Required(ErrorMessage = "Código de verificação do cartão é necessário.")]
        public string VerificationCode { get; set; }

        [Required(ErrorMessage = "Data de validade do cartão é necessário.")]
        public DateTime ExpirationDate { get; set; }
    }
}
