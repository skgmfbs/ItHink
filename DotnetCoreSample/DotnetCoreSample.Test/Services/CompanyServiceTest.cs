using AutoMapper;
using DotnetCoreSample.Core.Entities.Company;
using DotnetCoreSample.Core.Entities.Country;
using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Core.Services.Company;
using DotnetCoreSample.Core.Services.Country;
using DotnetCoreSample.Infrastructure.Repositories;
using DotnetCoreSample.Test.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DotnetCoreSample.Test.Services
{
    public class CompanyServiceTest : IClassFixture<TestContext>
    {
        private readonly TestContext testContext;

        public CompanyServiceTest(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Fact]
        public void CanGetAll()
        {
            testContext.RunInTestEnvironment(async (serviceFactory) =>
            {
                //Arrange 
                IService<Company> companyService = serviceFactory.CreateCompanyService();
                Country country = await CreateTestCountry(serviceFactory);
                Guid id = await companyService.Add(new AddCompany { Name = "Test Company", CountryId = country.Id });
                Assert.NotEqual(id, Guid.Empty);

                //Act
                IEnumerable<Company> countries = await companyService.GetAll();

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
                IService<Company> companyService = serviceFactory.CreateCompanyService();
                Country country = await CreateTestCountry(serviceFactory);
                Guid id = await companyService.Add(new AddCompany { Name = "Test Company", CountryId = country.Id });
                Assert.NotEqual(id, Guid.Empty);

                //Act
                Company company = await companyService.GetById(id);

                //Assert
                Assert.Equal("Test Company", company.Name);
                Assert.Equal(country.Id, company.CountryId);
            });
        }

        [Fact]
        public void CanAdd()
        {
            testContext.RunInTestEnvironment(async (serviceFactory) =>
            {
                //Arrange 
                IService<Company> companyService = serviceFactory.CreateCompanyService();
                Country country = await CreateTestCountry(serviceFactory);

                //Act
                Guid id = await companyService.Add(new AddCompany { Name = "Test Company", CountryId = country.Id });

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
                IService<Company> companyService = serviceFactory.CreateCompanyService();
                Country country = await CreateTestCountry(serviceFactory);
                Guid id = await companyService.Add(new AddCompany { Name = "Test Company", CountryId = country.Id });
                Assert.NotEqual(id, Guid.Empty);

                //Act
                await companyService.Update(new UpdateCompany { Id = id, Name = "Test Company Edit" });

                //Assert
                Company company = await companyService.GetById(id);
                Assert.Equal("Test Company Edit", company.Name);
            });
        }

        [Fact]
        public void CanDelete()
        {
            testContext.RunInTestEnvironment(async (serviceFactory) =>
            {
                //Arrange 
                IService<Company> companyService = serviceFactory.CreateCompanyService();
                Country country = await CreateTestCountry(serviceFactory);
                Guid id = await companyService.Add(new AddCompany { Name = "Test Company", CountryId = country.Id });
                Assert.NotEqual(id, Guid.Empty);

                //Act
                await companyService.Delete(id);

                //Assert
                Company company = await companyService.GetById(id);
                Assert.Null(company);
            });
        }

        private async Task<Country> CreateTestCountry(ServiceFactory serviceFactory)
        {
            ICountryService countryService = serviceFactory.CreateCountryService();
            Guid countryId = await countryService.Add(new AddCountry { Name = "Test Country", Abbreviation = "Test Abbreviation" });
            return await countryService.GetById(countryId);
        }
    }
}
