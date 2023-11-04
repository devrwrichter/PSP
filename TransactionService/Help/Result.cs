using FluentValidation.Results;
using TransactionService.ViewModels;

namespace TransactionService.Help
{
    /// <summary>
    /// A responsabilidade dessa classe é apenas de retorno do model ou da lista de erros 
    /// informando o state valido ou inválido.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : IResult<T> where T : IViewModel
    {
        /// <summary>
        /// Lista de erros
        /// </summary>
        public IList<string>? Errors { get; private set; }

        /// <summary>
        /// ViewModel
        /// </summary>
        public T? Model { get; }
        public bool Success { get { return Errors != null ? !Errors.Any() : true; } }


        /// <summary>
        /// Use esse ctor se quiser retornar apenas os erros.
        /// </summary>
        /// <param name="validationResult"></param>
        public Result(ValidationResult validationResult)
        {
            Errors = validationResult.GetResultErrors();
        }

        /// <summary>
        /// Use esse ctor para retornar apenas o model
        /// </summary>
        /// <param name="model"></param>
        public Result(T? model)
        {
            Model = model;
        }
    }
}
