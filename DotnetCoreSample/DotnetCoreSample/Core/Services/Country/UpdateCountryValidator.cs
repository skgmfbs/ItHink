using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Core.Services.Common.Validators;
using FluentValidation;

namespace DotnetCoreSample.Core.Services.Country
{
    public class UpdateCountryValidator : AbstractValidator<UpdateCountry>
    {
        public UpdateCountryValidator(ICountryService countryService)
        {
            RuleFor(x => x.Id).MustAsync(async (x, cancellation) =>
            {
                return (await countryService.GetById(x)) != null;
            }).WithMessage(x => $"Country ID :{x.Id} not found");

            RuleFor(x => x.Name).MustNotBeEmptyString().MaxLengthForName();
            RuleFor(x => x.Abbreviation).MustNotBeEmptyString();

            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
