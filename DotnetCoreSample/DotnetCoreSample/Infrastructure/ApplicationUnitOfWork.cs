using DotnetCoreSample.Core.Entities.Company;
using DotnetCoreSample.Core.Interfaces;
using DotnetCoreSample.Core.Interfaces.Repositories;
using DotnetCoreSample.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace DotnetCoreSample.Infrastructure
{
    public class ApplicationUnitOfWork : IApplicationUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        private IRepository<Company> companyRepository;
        private ICountryRepository countryRepository;
        
        public ApplicationUnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public IRepository<Company> CompanyRepository
        {
            get
            {
                if (companyRepository == null)
                {
                    companyRepository = new Repository<Company>(dbContext);
                }

                return companyRepository;
            }
        }

        public ICountryRepository CountryRepository
        {
            get
            {
                if (countryRepository == null)
                {
                    countryRepository = new CountryRepository(dbContext);
                }

                return countryRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
