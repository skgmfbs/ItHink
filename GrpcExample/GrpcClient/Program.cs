using Grpc.Core;
using Grpc.Net.Client;
using GrpcConfig.Definition;
using GrpcConfig.Services;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            await TestCompanyService(channel);
            await TestCountryService(channel);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static async Task TestCompanyService(GrpcChannel channel)
        {
            var serviceClient = new CompanyService.CompanyServiceClient(channel);

            await HandleException(nameof(GetCompaniesRequest), async () =>
            {
                using var getCompanies = serviceClient.GetCompanies(new GetCompaniesRequest());
                var responseStream = getCompanies.ResponseStream;
                while (await responseStream.MoveNext())
                {
                    var response = responseStream.Current;
                    Console.WriteLine($"[GetCompanies] Company name: {response.Name}.");
                }
            });

            await HandleException(nameof(GetCompanyByIdRequest), async () =>
            {
                var response = await serviceClient.GetCompanyByIdAsync(new GetCompanyByIdRequest {Id = 1});
                Console.WriteLine($"[GetCompanyById] Company name of Id '{response.Id}': {response.Name}.");
            });

            await HandleException(nameof(GetCompanyByCodeRequest), async () =>
            {
                using var getCompanyByCode = serviceClient.GetCompanyByCode();

                await getCompanyByCode.RequestStream.WriteAsync(new GetCompanyByCodeRequest {Code = "Comp1"});
                await getCompanyByCode.RequestStream.WriteAsync(new GetCompanyByCodeRequest {Code = "Comp2"});
                await getCompanyByCode.RequestStream.CompleteAsync();

                var responses = await getCompanyByCode.ResponseAsync;
                foreach (var response in responses.Companies)
                {
                    Console.WriteLine($"[GetCompanyByCode] Company name of Id '{response.Id}': {response.Name}.");
                }
            });

            await HandleException(nameof(GetCompanyByNameRequest), async () =>
            {
                using var getCompanyByName = serviceClient.GetCompanyByName();

                await getCompanyByName.RequestStream.WriteAsync(new GetCompanyByNameRequest {Name = "1"});
                await getCompanyByName.RequestStream.WriteAsync(new GetCompanyByNameRequest {Name = "2"});
                await getCompanyByName.RequestStream.CompleteAsync();

                while (await getCompanyByName.ResponseStream.MoveNext())
                {
                    var response = getCompanyByName.ResponseStream.Current;
                    Console.WriteLine($"Company Id: '{response.Id}', Company name: '{response.Name}'.");
                }
            });

            await HandleException(nameof(GetCompanyByIdRequest), async () =>
            {
                var response = await serviceClient.GetCompanyByIdAsync(new GetCompanyByIdRequest {Id = 3});
                Console.WriteLine($"Company name of Id '3': {response.Name}.");
            });
        }

        private static async Task TestCountryService(GrpcChannel channel)
        {
            var serviceClient = new CountryService.CountryServiceClient(channel);

            await HandleException(nameof(GetCountriesRequest), async () =>
            {
                using var getCountries = serviceClient.GetCountries(new GetCountriesRequest());
                var responseStream = getCountries.ResponseStream;
                while (await responseStream.MoveNext())
                {
                    var response = responseStream.Current;
                    Console.WriteLine($"[GetCountries] Country name: {response.Name}.");
                }
            });

            await HandleException(nameof(GetCountryByIdRequest), async () =>
            {
                var response = await serviceClient.GetCountryByIdAsync(new GetCountryByIdRequest { Id = 1 });
                Console.WriteLine($"[GetCountryById] Country name of Id '{response.Id}': {response.Name}.");
            });

            await HandleException(nameof(GetCountryByCodeRequest), async () =>
            {
                using var getCountryByCode = serviceClient.GetCountryByCode();

                await getCountryByCode.RequestStream.WriteAsync(new GetCountryByCodeRequest { Code = "TH" });
                await getCountryByCode.RequestStream.WriteAsync(new GetCountryByCodeRequest { Code = "PL" });
                await getCountryByCode.RequestStream.CompleteAsync();

                var responses = await getCountryByCode.ResponseAsync;
                foreach (var response in responses.Countries)
                {
                    Console.WriteLine($"[GetCountryByCode] Country name of Id '{response.Id}': {response.Name}.");
                }
            });

            await HandleException(nameof(GetCountryByNameRequest), async () =>
            {
                using var getCountryByName = serviceClient.GetCountryByName();

                await getCountryByName.RequestStream.WriteAsync(new GetCountryByNameRequest { Name = "1" });
                await getCountryByName.RequestStream.WriteAsync(new GetCountryByNameRequest { Name = "2" });
                await getCountryByName.RequestStream.CompleteAsync();

                while (await getCountryByName.ResponseStream.MoveNext())
                {
                    var response = getCountryByName.ResponseStream.Current;
                    Console.WriteLine($"Country Id: '{response.Id}', Country name: '{response.Name}'.");
                }
            });

            await HandleException(nameof(GetCountryByIdRequest), async () =>
            {
                var response = await serviceClient.GetCountryByIdAsync(new GetCountryByIdRequest { Id = 3 });
                Console.WriteLine($"Country name of Id '3': {response.Name}.");
            });
        }

        private static async Task HandleException(string requestName, Func<Task> callServiceClientFunc)
        {
            try
            {
                await callServiceClientFunc();
            }
            catch (RpcException e)
            {
                Console.WriteLine($"gRPC: '{requestName}' is failed.");
                Console.WriteLine($"Code: {e.Status.StatusCode}");
                Console.WriteLine($"Detail: {e.Status.Detail}");

            }
        }
    }
}
