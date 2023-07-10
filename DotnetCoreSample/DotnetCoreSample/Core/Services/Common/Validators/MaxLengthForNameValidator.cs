using DotnetCoreSample.Core.Services.Common.Validators.Constants;
using FluentValidation;
using FluentValidation.Validators;

namespace DotnetCoreSample.Core.Services.Common.Validators
{
    public class MaxLengthForNameValidator : PropertyValidator
    {
        internal MaxLengthForNameValidator()
            : base("{PropertyName}: the length of the string is larger than {MaxLength}.")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;

            if ((value?.Length ?? 0) > ValidationConstants.MaxLengthName)
            {
                context.MessageFormatter.AppendArgument("MaxLength", ValidationConstants.MaxLengthName);
                return false;
            }

            return true;
        }
    }

    internal static class MaxLengthForNameValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, string> MaxLengthForName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new MaxLengthForNameValidator());
        }
    }
}