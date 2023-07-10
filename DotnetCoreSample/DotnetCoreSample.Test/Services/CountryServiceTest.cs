using DotnetCoreSample.Core.Entities.Country;
using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Core.Services.Country;
using DotnetCoreSample.Test.Context;
using System;
using System.Collections.Generic;
using Xunit;

namespace DotnetCoreSample.Test.Services
{
    public class CountryServiceTest : IClassFixture<TestContext>
    {
        private readonly TestContext testContext;

        public CountryServiceTest(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Fact]
        public void CanGetAll()
        {
            testContext.RunInTestEnvironment(async (serviceFactory) =>
            {
                //Arrange 
                ICountryService countryService = serviceFactory.CreateCountryService();
                Guid id = await countryService.Add(new AddCountry { Name = "Test Country", Abbreviation = "Test Abbreviation" });
                Assert.NotEqual(id, Guid.Empty);

                //Act
                IEnumerable<Country> countries = await countryService.GetAll();

                //Assert
                Assert.NotNull(countries);
                Assert.Single(countries);
            });
        }

        [Fact]
        public void CanGetById()
        {
            testContext.RunInTestEnvironment(async (serviceFactory) =>
            {
                //Arrange 
                ICountryService countryService = serviceFactory.CreateCountryService();
                Guid id = await countryService.Add(new AddCountry { Name = "Test Country", Abbreviation = "Test Abbreviation" });
                Assert.NotEqual(id, Guid.Empty);

                //Act
                Country country = await countryService.GetById(id);

                //Assert
                Assert.Equal("Test Country", country.Name);
                Assert.Equal("Test Abbreviation", country.Abbreviation);
            });
        }

        [Fact]
        public void CanGetByAbbreviation()
        {
            testContext.RunInTestEnvironment(async (serviceFactory) =>
            {
                //Arrange 
                ICountryService countryService = serviceFactory.CreateCountryService();
                Guid id = await countryService.Add(new AddCountry { Name = "Test Country", Abbreviation = "Test Abbreviation" });
                Assert.NotEqual(id, Guid.Empty);

                //Act
                IEnumerable<Country> countries = await countryService.GetByAbbreviation("Test Abbreviation");

                //Assert
                Assert.NotNull(countries);
                Assert.Single(countries);
            });
        }

        [Fact]
        public void CanAdd()
        {
            testContext.RunInTestEnvironment(async (serviceFactory) =>
            {
                //Arrange 
                ICountryService countryService = serviceFactory.CreateCountryService();

                //Act
                Guid id = await countryService.Add(new AddCountry { Name = "Test Country", Abbreviation = "Test Abbreviation" });

                //Assert
                Assert.NotEqual(id, Guid.Empty);
            });
        }

        [Fact]
        public void CanUpdate()
        {
            testContext.RunInTestEnvironment(async (serviceFactory) =>
            {
                //Arrange 
                ICountryService countryService = serviceFactory.CreateCountryService();
                Guid id = await countryService.Add(new AddCountry { Name = "Test Country", Abbreviation = "Test Abbreviation" });
                Assert.NotEqual(id, Guid.Empty);

                //Act
                await countryService.Update(new UpdateCountry { Id = id, Name = "Test Country Edit", Abbreviation = "Test Abbreviation Edit" });
                
                //Assert
                Country country = await countryService.GetById(id);
                Assert.Equal("Test Country Edit", country.Name);
                Assert.Equal("Test Abbreviation Edit", country.Abbreviation);
            });
        }

        [Fact]
        public void CanDelete()
        {
            testContext.RunInTestEnvironment(async (serviceFactory) =>
            {
                //Arrange 
                ICountryService countryService = serviceFactory.CreateCountryService();
                Guid id = await countryService.Add(new AddCountry { Name = "Test Country", Abbreviation = "Test Abbreviation" });
                Assert.NotEqual(id, Guid.Empty);
                Country country = await countryService.GetById(id);
                Assert.NotNull(country);

                //Act
                await countryService.Delete(id);
                
                //Assert
                country = await countryService.GetById(id);
                Assert.Null(country);
            });
        }
    }
}
