using DotnetCoreSample.Core.Entities.Company;
using DotnetCoreSample.Core.Services.Company;
using DotnetCoreSample.Test.Context;
using DotnetCoreSample.Test.Mock;
using FluentValidation;
using FluentValidation.Results;
using System;
using Xunit;

namespace DotnetCoreSample.Test.Validators
{
    public class AddCompanyValidatorTest : IClassFixture<TestContext>
    {
        private readonly TestContext testContext;
        private readonly ServiceMock serviceMockFactory;
        
        public AddCompanyValidatorTest(TestContext testContext)
        {
            this.testContext = testContext;
            this.serviceMockFactory = testContext.CreateServicesMock();
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(
            "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
            "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
            "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789",
            false)]
        [InlineData("Test Name", true)]
        public void CanValidateName(string name, bool expected)
        {
            //Arrange 
            IValidator<AddCompany> validator = GetValidator();
            AddCompany addCompany = new AddCompany
            {
                Name = name,
                CountryId = serviceMockFactory.CountryMock.ValidId
            };
            //Act
            ValidationResult result = validator.Validate(addCompany);

            //Assert
            Assert.Equal(result.IsValid, expected);
        }

        [Fact]
        public void CanValidateCountryId()
        {
            //Arrange 
            IValidator<AddCompany> validator = GetValidator();
            AddCompany addCompany = new AddCompany
            {
                Name = "Test Name",
                CountryId = serviceMockFactory.CountryMock.ValidId
            };
            //Act
            ValidationResult result = validator.Validate(addCompany);

            //Assert
            Assert.True(result.IsValid);
            
            //Arrange 
            addCompany.CountryId = Guid.NewGuid();
            //Act
            result = validator.Validate(addCompany);

            //Assert
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData(null, "Test City", "12345", false)]
        [InlineData("", "Test City", "12345", false)]
        [InlineData("Test Street", null, "12345", false)]
        [InlineData("Test Street", "", "12345", false)]
        [InlineData("Test Street", "Test City", null, false)]
        [InlineData("Test Street", "Test City", "", false)]
        [InlineData("Test Street", "Test City", "12345", true)]
        public void CanValidateAddress(string street, string city, string zipCode, bool expected)
        {
            //Arrange 
            IValidator<AddCompany> validator = GetValidator();
            AddCompany addCompany = new AddCompany
            {
                Name = "Test Name",
                CountryId = serviceMockFactory.CountryMock.ValidId,
                Address = new Address { Street = street, City = city, ZipCode = zipCode }
            };
            //Act
            ValidationResult result = validator.Validate(addCompany);

            //Assert
            Assert.Equal(result.IsValid, expected);
        }

        private IValidator<AddCompany> GetValidator()
        {
            return new AddCompanyValidator(serviceMockFactory.CountryMock.Service);
        }
    }
}