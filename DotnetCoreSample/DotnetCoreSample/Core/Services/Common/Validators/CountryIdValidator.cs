using DotnetCoreSample.Core.Interfaces.Services;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCoreSample.Core.Services.Common.Validators
{
    public class CountryIdValidator : PropertyValidator
    {
        private readonly ICountryService countryService;

        internal CountryIdValidator(ICountryService countryService)
            : base("{PropertyName}: '{PropertyValue}' is not valid.")
        {
            this.countryService = countryService;
        }

        public override bool ShouldValidateAsync(ValidationContext context)
        {
            return true;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            throw new NotImplementedException();
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            return context.PropertyValue != null && (await countryService.GetById(Guid.Parse(context.PropertyValue.ToString()))) != null;
        }
    }

    internal static class CategoryIdValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, Guid?> MustBeValidCountryId<T>(this IRuleBuilder<T, Guid?> ruleBuilder, ICountryService countryService)
        {
            return ruleBuilder.SetValidator(new CountryIdValidator(countryService));
        }
    }
}
