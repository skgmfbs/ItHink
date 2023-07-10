using DotnetCoreSample.Core.Entities.Country;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSample.Core.Interfaces.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<IEnumerable<Country>> GetByAbbreviation(string abbreviation);
    }
}
