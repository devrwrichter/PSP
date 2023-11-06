using FluentValidation;
using Stone.PSP.Domain.Validators.PropertyValidators;

namespace Stone.PSP.Domain.Validators
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, byte> CheckIntValueInEnum<T, TE>(this IRuleBuilder<T, byte> ruleBuilder) where TE : struct
        {
            return ruleBuilder.SetValidator(new ValidatorIfNullIntExistsInEnumerator<T, byte, TE>());
        }
    }
}
