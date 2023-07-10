using DotnetCoreSample.Core.Entities.Country;
using DotnetCoreSample.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreSample.Infrastructure.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<Country>> GetByAbbreviation(string abbreviation)
        {
            return await DbSet.Where(c => !string.IsNullOrEmpty(c.Abbreviation)).ToListAsync();
        }
    }
}
