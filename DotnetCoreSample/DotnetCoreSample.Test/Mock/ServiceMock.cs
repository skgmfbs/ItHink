using DotnetCoreSample.Infrastructure;

namespace DotnetCoreSample.Test.Mock
{
    public class ServiceMock
    {
        private readonly ApplicationDbContext applicationDbContext;

        private CompanyServiceMock companyServiceMock;
        private CountryServiceMock countryServiceMock;
        
        public ServiceMock(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public CompanyServiceMock CompanyMock => companyServiceMock ?? (companyServiceMock = new CompanyServiceMock());
        public CountryServiceMock CountryMock => countryServiceMock ?? (countryServiceMock = new CountryServiceMock());
    }
}
