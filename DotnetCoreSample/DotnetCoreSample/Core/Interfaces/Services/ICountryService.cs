using DotnetCoreSample.Core.Entities.Country;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSample.Core.Interfaces.Services
{
    public interface ICountryService: IService<Country>
    {
        Task<IEnumerable<Country>> GetByAbbreviation(string abbreviation);
    }
}
