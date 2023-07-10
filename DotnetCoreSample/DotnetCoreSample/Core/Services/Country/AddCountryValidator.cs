using DotnetCoreSample.Core.Services.Common.Validators;
using FluentValidation;

namespace DotnetCoreSample.Core.Services.Country
{
    public class AddCountryValidator : AbstractValidator<AddCountry>
    {
        public AddCountryValidator()
        {
            RuleFor(x => x.Name).MustNotBeEmptyString().MaxLengthForName();
            RuleFor(x => x.Abbreviation).MustNotBeEmptyString();

            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
