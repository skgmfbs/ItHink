using System;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcConfig.Definition;

namespace GrpcServer.Services
{
    public class CountryService : GrpcConfig.Services.CountryService.CountryServiceBase
    {
        private readonly ILogger<CountryService> _logger;

        public CountryService(ILogger<CountryService> logger)
        {
            _logger = logger;
        }

        public override async Task GetCountries(
            GetCountriesRequest request,
            IServerStreamWriter<CountryResponse> responseStream, 
            ServerCallContext context)
        {
            foreach (var response in CompaniesMock())
            {
                await responseStream.WriteAsync(response);
            }
        }

        public override Task<CountryResponse> GetCountryById(
            GetCountryByIdRequest request, 
            ServerCallContext context)
        {
            var mock = CompaniesMock();
            var response = mock.SingleOrDefault(m => m.Id == request.Id);
            if (response == null)
            {
                var countryIdNotFoundMessage = $"Country Id '{request.Id}' not found.";
                _logger.LogError(countryIdNotFoundMessage);
                throw new RpcException(new Status(StatusCode.NotFound, countryIdNotFoundMessage));
            }

            return Task.FromResult(response);
        }

        public override async Task<CountriesResponse> GetCountryByCode(
            IAsyncStreamReader<GetCountryByCodeRequest> requestStream,
            ServerCallContext context)
        {
            var countriesResponse = new CountriesResponse();
            while (await requestStream.MoveNext())
            {
                var request = requestStream.Current;
                var response = CompaniesMock().SingleOrDefault(m => m.Code.Equals(request.Code, StringComparison.InvariantCultureIgnoreCase));
                if (response != null)
                {
                    countriesResponse.Countries.Add(response);
                }
            }

            return countriesResponse;
        }

        public override async Task GetCountryByName(
            IAsyncStreamReader<GetCountryByNameRequest> requestStream,
            IServerStreamWriter<CountryResponse> responseStream, 
            ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var request = requestStream.Current;
                var countryResponses = CompaniesMock().Where(m => m.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));
                foreach (var countryResponse in countryResponses)
                {
                    await responseStream.WriteAsync(countryResponse);
                }
            }
        }

        private static IEnumerable<CountryResponse> CompaniesMock()
        {
            return new List<CountryResponse>
            {
                new CountryResponse
                {
                    Id = 1,
                    Code = "TH",
                    Name = "Thailand"
                },
                new CountryResponse
                {
                    Id = 2,
                    Code = "PL",
                    Name = "Poland"
                }
            };
        }
    }
}
