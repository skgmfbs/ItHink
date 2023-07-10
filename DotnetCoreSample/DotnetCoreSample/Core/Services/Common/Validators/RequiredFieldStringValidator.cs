using FluentValidation;
using FluentValidation.Validators;

namespace DotnetCoreSample.Core.Services.Common.Validators
{
    public class RequiredFieldStringValidator : PropertyValidator
    {
        internal RequiredFieldStringValidator()
            : base("{PropertyName}: is required field.")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return !string.IsNullOrEmpty(context.PropertyValue as string);
        }
    }

    internal static class RequiredFieldStringValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, string> MustNotBeEmptyString<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new RequiredFieldStringValidator());
        }
    }
}
