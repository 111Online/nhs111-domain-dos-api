using System.Threading.Tasks;
using DirectoryOfServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NHS111.Domain.Dos.Api.Functional.Test.RestTools;
using NHS111.Domain.Dos.Api.Models.Request;
using NHS111.Domain.Dos.Api.Models.Response;
using NUnit.Framework;
using RestSharp;

namespace NHS111.Domain.Dos.Api.Functional.Test
{
    public class DomainDosApiTests
    {
        private IRestClient _restClient;
        private IConfiguration _config;

        public DomainDosApiTests()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("C:\\Configurations\\nhs111-shared-resources\\appsettings.debug.json", optional: true)
                .Build();
        }

        private string DosApiUsername
        {
            get { return _config["DosCredentialUser"]; }
        }

        private string DosApiPassword
        {
            get { return _config["DosCredentialPassword"]; }
        }

        private string DomainDosSApiCheckCapacitySummaryUrl
        {
            get { return _config["DomainDosApiCheckCapacitySummaryUrl"]; }
        }

        private string DomainDosApiServiceDetailsByIdUrl
        {
            get { return _config["DomainDosApiServiceDetailsByIdUrl"]; }
        }

        [SetUp]
        public void SetUp()
        {
            _restClient = new RestClient(_config["DomainDosApiBaseUrl"]);
            _restClient.AddHandler("application/json", () => NewtonsoftJsonSerializer.Default);
        }

        [Test]
        public async Task TestCheckCapacitySumary()
        {
            var checkCapacitySummaryRequest = new DosCheckCapacitySummaryRequest(DosApiUsername, DosApiPassword, new DosCase { Age = "22", Gender = "F", PostCode = "HP21 8AL", Disposition = 1032, SymptomGroup = 1206, SymptomDiscriminatorList = new [] { 4193 }});
            var json = JsonConvert.SerializeObject(checkCapacitySummaryRequest);
            var request = new RestRequest(DomainDosSApiCheckCapacitySummaryUrl, Method.POST);
            request.AddJsonBody(json);
            var result = await _restClient.ExecuteTaskAsync<DosCheckCapacitySummaryResponse>(request);
            Assert.IsTrue(result.IsSuccessful);
            SchemaValidation.AssertValidResponseSchema(result.Content, SchemaValidation.ResponseSchemaType.CheckCapacitySummary);
        }

        [Test]
        public async Task TestCheckServiceDetailsById()
        {
            var serviceDetailsByIdRequest = new DosServiceDetailsByIdRequest(DosApiUsername, DosApiPassword, "1315835856");
            var request = new RestRequest(DomainDosApiServiceDetailsByIdUrl, Method.POST);
            request.AddJsonBody(serviceDetailsByIdRequest);
            var result = await _restClient.ExecuteTaskAsync<DosServiceDetailsByIdResponse>(request);
            Assert.IsTrue(result.IsSuccessful);
            SchemaValidation.AssertValidResponseSchema(result.Content, SchemaValidation.ResponseSchemaType.CheckServiceDetailsById);
        }
    }
}