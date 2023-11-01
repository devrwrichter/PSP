using FluentValidation;
using Stone.PSP.Domain.Validators.PropertyValidators;

namespace Stone.PSP.Domain.Validators
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, int> CheckIntValueInEnum<T, TE>(this IRuleBuilder<T, int> ruleBuilder) where TE : struct
        {
            return ruleBuilder.SetValidator(new ValidatorIfNullIntExistsInEnumerator<T, int, TE>());
        }
    }
    }
