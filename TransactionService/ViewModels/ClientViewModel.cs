using System.ComponentModel.DataAnnotations;

namespace TransactionService.ViewModels
{
    public class ClientViewModel : IViewModel
    {
        [Required(ErrorMessage = "O código do cliente não pode ser vazio")]
        public Guid Id { get; set; }
    }
}
