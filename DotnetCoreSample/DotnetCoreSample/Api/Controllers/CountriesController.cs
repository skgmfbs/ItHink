using DotnetCoreSample.Core.Entities.Country;
using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Core.Services.Country;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSample.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountriesController : ApiControllerBase<Country, AddCountry, UpdateCountry>
    {
        public CountriesController(ICountryService service) : base(service)
        {
        }

        [HttpGet("getbyabbreviation")]
        public Task<IEnumerable<Country>> GetByAbbreviation([FromQuery] string abbreviation)
        {
            return (service as ICountryService).GetByAbbreviation(abbreviation);
        }
    }
}
