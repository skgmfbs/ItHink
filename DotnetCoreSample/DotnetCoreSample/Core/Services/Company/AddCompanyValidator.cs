using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Core.Services.Common.Validators;
using FluentValidation;

namespace DotnetCoreSample.Core.Services.Company
{
    public class AddCompanyValidator : AbstractValidator<AddCompany>
    {
        public AddCompanyValidator(ICountryService countryService) : base()
        {
            RuleFor(x => x.Name).MustNotBeEmptyString().MaxLengthForName();
            RuleFor(x => x.CountryId).NotNull().MustBeValidCountryId(countryService);
            RuleFor(x => x.Address).SetValidator(new AddressValidator()).When(x => x.Address != null);

            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
