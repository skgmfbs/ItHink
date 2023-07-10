using Autofac;
using Autofac.Extensions.DependencyInjection;
using DotnetCoreSample.Core.Interfaces;
using DotnetCoreSample.Core.Interfaces.Repositories;
using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Infrastructure;
using DotnetCoreSample.Infrastructure.Repositories;
using DotnetCoreSample.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using DotnetCoreSample.Core.Services.Country;
using FluentValidation;
using DotnetCoreSample.Core.Services.Company;

namespace DotnetCoreSample.Api.Startup
{
    public static class AddCustomServicesExtension
    {
        public static IServiceProvider AddCustomServices(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>().InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterType<CountryService>().As<ICountryService>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
            
            builder.RegisterType<AddCompanyValidator>().As<IValidator<AddCompany>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateCompanyValidator>().As<IValidator<UpdateCompany>>().InstancePerLifetimeScope();
            builder.RegisterType<AddCountryValidator>().As<IValidator<AddCountry>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateCountryValidator>().As<IValidator<UpdateCountry>>().InstancePerLifetimeScope();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(builder.Build());
        }
    }
}
