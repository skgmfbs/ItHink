using DotnetCoreSample.Core.Entities.Company;
using DotnetCoreSample.Core.Services.Common.Validators;
using FluentValidation;

namespace DotnetCoreSample.Core.Services.Company
{
    public class AddressValidator : AbstractValidator<Address>
    {
        internal AddressValidator()
        {
            RuleFor(x => x.Street).MustNotBeEmptyString();
            RuleFor(x => x.City).MustNotBeEmptyString();
            RuleFor(x => x.ZipCode).MustNotBeEmptyString();

            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}

