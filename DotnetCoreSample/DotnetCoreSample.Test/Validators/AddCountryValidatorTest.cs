using DotnetCoreSample.Core.Services.Country;
using DotnetCoreSample.Test.Context;
using DotnetCoreSample.Test.Mock;
using FluentValidation;
using FluentValidation.Results;
using Xunit;

namespace DotnetCoreSample.Test.Validators
{
    public class AddCountryValidatorTest : IClassFixture<TestContext>
    {
        private readonly TestContext testContext;
        private readonly ServiceMock serviceMockFactory;
        

        public AddCountryValidatorTest(TestContext testContext)
        {
            this.testContext = testContext;
            this.serviceMockFactory = testContext.CreateServicesMock();
        }

        [Theory]
        [InlineData(null, "Test Abbreviation", false)]
        [InlineData("", "Test Abbreviation", false)]
        [InlineData(
            "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
            "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
            "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789",
            "Test Abbreviation",
            false)]
        [InlineData("Test Name", null, false)]
        [InlineData("Test Name", "", false)]
        [InlineData("Test Name", "Test Abbreviation", true)]
        public void CanValidateAddCountry(string name, string abbreviation, bool expected)
        {
            //Arrange 
            IValidator<AddCountry> validator = GetValidator();
            AddCountry addCountry = new AddCountry
            {
                Name = name,
                Abbreviation = abbreviation
            };
            //Act
            ValidationResult result = validator.Validate(addCountry);

            //Assert
            Assert.Equal(result.IsValid, expected);
        }

        private IValidator<AddCountry> GetValidator()
        {
            return new AddCountryValidator();
        }
    }
}