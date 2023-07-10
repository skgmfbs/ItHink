using AutoMapper;
using DotnetCoreSample.Core.Common;
using DotnetCoreSample.Core.Entities.Company;
using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Core.Services;
using DotnetCoreSample.Core.Services.Country;
using DotnetCoreSample.Infrastructure;
using DotnetCoreSample.Infrastructure.Repositories;
using System;

namespace DotnetCoreSample.Test.Services
{
    public class ServiceFactory
    {
        private readonly ApplicationDbContext dbContext;

        public ServiceFactory(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICountryService CreateCountryService()
        {
            return new CountryService(new CountryRepository(dbContext), Mapper.Instance);
        }

        public IService<Company> CreateCompanyService()
        {
            return CreateService<Company>();
        }

        public IService<TEntity> CreateService<TEntity>() where TEntity : EntityBase<Guid>
        {
            return new Service<TEntity>(new Repository<TEntity>(dbContext), Mapper.Instance);
        }
    }
}
