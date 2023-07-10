using System;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcConfig.Definition;

namespace GrpcServer.Services
{
    public class CompanyService : GrpcConfig.Services.CompanyService.CompanyServiceBase
    {
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(ILogger<CompanyService> logger)
        {
            _logger = logger;
        }

        public override async Task GetCompanies(
            GetCompaniesRequest request,
            IServerStreamWriter<CompanyResponse> responseStream, 
            ServerCallContext context)
        {
            foreach (var response in CompaniesMock())
            {
                await responseStream.WriteAsync(response);
            }
        }

        public override Task<CompanyResponse> GetCompanyById(
            GetCompanyByIdRequest request, 
            ServerCallContext context)
        {
            var response = CompaniesMock().SingleOrDefault(m => m.Id == request.Id);
            if (response == null)
            {
                var companyIdNotFoundMessage = $"Company Id '{request.Id}' not found.";
                _logger.LogError(companyIdNotFoundMessage);
                throw new RpcException(new Status(StatusCode.NotFound, companyIdNotFoundMessage));
            }

            return Task.FromResult(response);
        }

        public override async Task<CompaniesResponse> GetCompanyByCode(
            IAsyncStreamReader<GetCompanyByCodeRequest> requestStream,
            ServerCallContext context)
        {
            var companiesResponse = new CompaniesResponse();
            while (await requestStream.MoveNext())
            {
                var request = requestStream.Current;
                var response = CompaniesMock().SingleOrDefault(m => m.Code.Equals(request.Code, StringComparison.InvariantCultureIgnoreCase));
                if (response != null)
                {
                    companiesResponse.Companies.Add(response);
                }
            }

            return companiesResponse;
        }

        public override async Task GetCompanyByName(
            IAsyncStreamReader<GetCompanyByNameRequest> requestStream,
            IServerStreamWriter<CompanyResponse> responseStream, 
            ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var request = requestStream.Current;
                var responses = CompaniesMock().Where(m => m.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));
                foreach (var response in responses)
                {
                    await responseStream.WriteAsync(response);
                }
            }
        }

        private static IEnumerable<CompanyResponse> CompaniesMock()
        {
            return new List<CompanyResponse>
            {
                new CompanyResponse
                {
                    Id = 1,
                    Code = "Comp1",
                    Name = "Test Company 1"
                },
                new CompanyResponse
                {
                    Id = 2,
                    Code = "Comp2",
                    Name = "Test Company 2"
                }
            };
        }
    }
}
