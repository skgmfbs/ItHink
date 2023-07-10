using DotnetCoreSample.Core.Services.Country;
using DotnetCoreSample.Test.Context;
using DotnetCoreSample.Test.Mock;
using FluentValidation;
using FluentValidation.Results;
using System;
using Xunit;

namespace DotnetCoreSample.Test.Validators
{
    public class UpdateCountryValidatorTest : IClassFixture<TestContext>
    {
        private readonly TestContext testContext;
        private readonly ServiceMock serviceMockFactory;
        

        public UpdateCountryValidatorTest(TestContext testContext)
        {
            this.testContext = testContext;
            this.serviceMockFactory = testContext.CreateServicesMock();
        }

        [Fact]
        public void CanValidateId()
        {
            //Arrange 
            IValidator<UpdateCountry> validator = GetValidator();

            UpdateCountry updateCountry = new UpdateCountry
            {
                Id = serviceMockFactory.CountryMock.ValidId,
                Name = "Test Name",
                Abbreviation = "Test Abbreviation"
            };
            //Act
            ValidationResult result = validator.Validate(updateCountry);

            //Assert
            Assert.True(result.IsValid);

            //Arrange 
            updateCountry.Id = Guid.NewGuid();
            //Act
            result = validator.Validate(updateCountry);

            //Assert
            Assert.False(result.IsValid);
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
        public void CanValidateUpdateCountry(string name, string abbreviation, bool expected)
        {
            //Arrange 
            IValidator<UpdateCountry> validator = GetValidator();
            UpdateCountry addCountry = new UpdateCountry
            {
                Id = serviceMockFactory.CountryMock.ValidId,
                Name = name,
                Abbreviation = abbreviation
            };
            //Act
            ValidationResult result = validator.Validate(addCountry);

            //Assert
            Assert.Equal(result.IsValid, expected);
        }

        private IValidator<UpdateCountry> GetValidator()
        {
            //Arrange 
            return new UpdateCountryValidator(serviceMockFactory.CountryMock.Service);
        }
    }
}