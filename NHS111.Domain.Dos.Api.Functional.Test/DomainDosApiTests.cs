using Microsoft.Extensions.Configuration;
using NHS111.Domain.Dos.Api.Functional.Test.RestTools;
using NHS111.Domain.Dos.Api.Models.Request;
using NUnit.Framework;
using PathwayService;
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
            get { return _config["DosCredentialUser"]; }
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
        public async void TestCheckCapacitySumary()
        {
            var checkCapacitySummaryRequest = new DosCheckCapacitySummaryRequest(DosApiUsername, DosApiPassword, new DosCase { Age = "22", Gender = "F", PostCode = "HP21 8AL" });
            var request = new RestRequest(DomainDosSApiCheckCapacitySummaryUrl, Method.POST);
            request.AddJsonBody(checkCapacitySummaryRequest);
            var result = await _restClient.ExecuteTaskAsync<CheckCapacitySummaryResponse>(request);
            Assert.IsTrue(result.IsSuccessful);
            SchemaValidation.AssertValidResponseSchema(result.Content, SchemaValidation.ResponseSchemaType.CheckCapacitySummary);

            //var firstService = result.Data.CheckCapacitySummaryResult[0];
            //AssertResponse(firstService);
        }

        [Test]
        public async void TestCheckServiceDetailsById()
        {
            var serviceDetailsByIdRequest = new DosServiceDetailsByIdRequest(DosApiUsername, DosApiPassword, "1315835856");
            var request = new RestRequest(DomainDosApiServiceDetailsByIdUrl, Method.POST);
            request.AddJsonBody(serviceDetailsByIdRequest);
            var result = await _restClient.ExecuteTaskAsync<ServiceDetailsByIdResponse>(request);

            Assert.IsTrue(result.IsSuccessful);
            SchemaValidation.AssertValidResponseSchema(result.Content, SchemaValidation.ResponseSchemaType.CheckServiceDetailsById);
        }
    }
}