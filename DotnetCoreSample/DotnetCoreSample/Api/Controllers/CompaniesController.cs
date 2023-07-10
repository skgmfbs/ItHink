using DotnetCoreSample.Core.Entities.Company;
using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Core.Services.Company;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreSample.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompaniesController : ApiControllerBase<Company, AddCompany, UpdateCompany>
    {
        public CompaniesController(IService<Company> service) : base(service)
        {
        }
    }
}
