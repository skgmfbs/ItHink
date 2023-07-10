using AutoMapper;
using DotnetCoreSample.Core.Interfaces.Repositories;
using DotnetCoreSample.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSample.Core.Services.Country
{
    public class CountryService : Service<Entities.Country.Country>, ICountryService
    {
        private readonly ICountryRepository countryRepository;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
            : base(countryRepository, mapper)
        {
            this.countryRepository = countryRepository;
        }

        public Task<IEnumerable<Entities.Country.Country>> GetByAbbreviation(string abbreviation)
        {
            return countryRepository.GetByAbbreviation(abbreviation);
        }
    }
}
