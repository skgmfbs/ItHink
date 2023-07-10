using DotnetCoreSample.Core.Entities.Company;
using DotnetCoreSample.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace DotnetCoreSample.Core.Interfaces
{
    public interface IApplicationUnitOfWork
    {
        IRepository<Company> CompanyRepository { get; }
        ICountryRepository CountryRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
