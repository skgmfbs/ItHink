using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Core.Services.Common.Validators;
using FluentValidation;
using System;

namespace DotnetCoreSample.Core.Services.Company
{
    public class UpdateCompanyValidator : AbstractValidator<UpdateCompany>
    {
        public UpdateCompanyValidator(IService<Entities.Company.Company> companyService, ICountryService countryService)
        {
            RuleFor(x => x.Id).MustAsync(async (x, cancellation) =>
            {
                return (await companyService.GetById(x)) != null;
            }).WithMessage(x => $"Company ID :{x.Id} not found");

            RuleFor(x => x.Name).MustNotBeEmptyString().MaxLengthForName();
            RuleFor(x => x.CountryId).MustBeValidCountryId(countryService).When(x => x.CountryId.GetValueOrDefault() != Guid.Empty);
            RuleFor(x => x.Address).SetValidator(new AddressValidator()).When(x => x.Address != null);

            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
