using FluentValidation;
using FluentValidation.Validators;
using Stone.PSP.Domain.Help;

namespace Stone.PSP.Domain.Validators.PropertyValidators
{
    internal class ValidatorIfNullIntExistsInEnumerator<T, T1, TEnum> : IPropertyValidator<T, byte> where TEnum : struct
    {
        public string Name => "ValidatorIfNullIntExistsInEnumerator";

        public string GetDefaultMessageTemplate(string errorCode)
        {
            return $"Value not exists in enumerator {typeof(TEnum).Name}";
        }

        public bool IsValid(ValidationContext<T> context, byte value)
        {
            return EnumUtils.ExistsInEnum<TEnum>(value);
        }
    }
}
