using FluentValidation;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Enumerators;
using System.Text.RegularExpressions;

namespace Stone.PSP.Domain.Validators
{
    public class TransactionValidator : AbstractValidator<PspTransaction>
    {
        #region Constants

        //Essas constantes são public porque eu uso nos testes unitários, fica melhor assim quando mudamos algo, já reflete nos testes.

        public const string ErrorCardHolderEmpty = "Portador do cartão é necessário.";
        public const string ErrorPaymentMethodCode = "Não existe essa forma de pagamento, favor informar 1 para Crédito ou 2 para Débito.";
        public const string ErrorCardVerificationCode = "O código de verificação é inválido.";
        public const string ErrorCardValidateDate = "A data de validade do cartão precisa ser maior que a data atual.";
        public const string ErrorInvalidClientId = "O Id do cliente precisa ser informado.";
        public const string ErrorValueIsMinorOrEqualThanZero = "O valor precisa ser superior a 0.0.";
        public const string ErrorDescriptionEmpty = "A descrição é um campo obrigatório.";
        public const string ErrorCardNumberEmpty = "O número do cartão de crédito precisa ser informado.";
        public const string ErrorInvalidDescriptionLength = $"O tamanho da descrição precisa estar entre 5 e 200";

        public const int MaxLengthDescription = 100;
        public const int MinLengthDescription = 5;

        #endregion

        public TransactionValidator()
        {
            this.RuleFor(x => x.Value).GreaterThan(decimal.Zero).WithMessage(ErrorValueIsMinorOrEqualThanZero);
            this.RuleFor(x => x.CardHolder).NotEmpty().WithMessage(ErrorCardHolderEmpty);
            this.RuleFor(x => x.ClientId).Must(x => IsClientIdNotEmpty(x)).WithMessage(ErrorInvalidClientId);
            this.RuleFor(x => x.CardNumber).NotEmpty().WithMessage(ErrorCardNumberEmpty);
            this.RuleFor(x => x.Description).NotEmpty().WithMessage(ErrorDescriptionEmpty);

            When(x => !string.IsNullOrEmpty(x.Description), () =>
            {
                this.RuleFor(x => x.Description)
                .MinimumLength(MinLengthDescription)
                .WithMessage(ErrorInvalidDescriptionLength);

                this.RuleFor(x => x.Description)
                    .MaximumLength(MaxLengthDescription)
                    .WithMessage(ErrorInvalidDescriptionLength);
            });

            this.RuleFor(x => x.PaymentMethodCode)
                .CheckIntValueInEnum<PspTransaction, PaymentMethodCodeType>()
                .WithMessage(ErrorPaymentMethodCode);

            this.RuleFor(x => x.CardVerificationCode).NotNull()
               .WithMessage(ErrorCardVerificationCode);

            When(x => !string.IsNullOrEmpty(x.CardVerificationCode), () =>
            {
                this.RuleFor(x => x.CardVerificationCode)
                .Must(x => IsValidCardVerificationCode(x))
                .WithMessage(ErrorCardVerificationCode);
            });

            this.RuleFor(x => x.CardExpirationDate)
                .Must(x => IsValidCardValidateDate(x))
                .WithMessage(ErrorCardValidateDate);

            //Não coloquei todas as validações possíveis, apenas algumas para ilustrar, senão ficaria muito grande a parte de testes unitários.
        }

        private bool IsClientIdNotEmpty(Guid clientId)
        {
            return clientId != Guid.Empty;
        }

        private bool IsValidCardValidateDate(DateTime expirationDate)
        {
            return expirationDate > DateTime.Today;
        }

        private bool IsValidCardVerificationCode(string cvc)
        {
            var onlyNumbers = cvc.All(c => char.IsDigit(c));
            return cvc.Length >= 3 && cvc.Length <= 4 && onlyNumbers == true;
        }
    }
}
