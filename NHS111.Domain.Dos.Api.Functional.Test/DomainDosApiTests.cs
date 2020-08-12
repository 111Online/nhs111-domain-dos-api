using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using DirectoryOfServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NHS111.Domain.Dos.Api.Functional.Test.RestTools;
using NHS111.Domain.Dos.Api.Mappers;
using NHS111.Domain.Dos.Api.Models.Request;
using NHS111.Domain.Dos.Api.Models.Response;
using NUnit.Framework;
using RestSharp;

namespace NHS111.Domain.Dos.Api.Functional.Test
{
    public class DomainDosApiTests
    {
        private string CheckCapacitySummaryResponse => ReadJsonFile("NHS111.Domain.Dos.Api.Functional.Test.Json.CheckCapacitySummaryResponse.json");
        private string DosCheckCapacitySummaryResponse => ReadJsonFile("NHS111.Domain.Dos.Api.Functional.Test.Json.DosCheckCapacitySummaryResponse.json");
        private string DosServiceDetailsByIdResponse => ReadJsonFile("NHS111.Domain.Dos.Api.Functional.Test.Json.DosServiceDetailsByIdResponse.json");
        private string ServiceDetailsByIdResponse => ReadJsonFile("NHS111.Domain.Dos.Api.Functional.Test.Json.ServiceDetailsByIdResponse.json");

        [Test]
        public void test_check_capacity_summary_response_mapper()
        {
            var checkCapacitySummaryResponse = JsonConvert.DeserializeObject<CheckCapacitySummaryResponse>(CheckCapacitySummaryResponse);
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper();

            var dosCheckCapacitySummaryResponse = mapper.Map<DosCheckCapacitySummaryResponse>(checkCapacitySummaryResponse);
            var jsonDosCheckCapacitySummaryResponse = JsonConvert.SerializeObject(dosCheckCapacitySummaryResponse);
            Assert.AreEqual(jsonDosCheckCapacitySummaryResponse, DosCheckCapacitySummaryResponse);
        }

        [Test]
        public void test_service_details_by_id_mapper()
        {
            var serviceDetailsByIdResponse = JsonConvert.DeserializeObject<ServiceDetailsByIdResponse>(ServiceDetailsByIdResponse);
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper();

            var dosServiceDetailsByIdResponse = mapper.Map<DosServiceDetailsByIdResponse>(serviceDetailsByIdResponse);
            var jsonDosServiceDetailsByIdResponse = JsonConvert.SerializeObject(dosServiceDetailsByIdResponse);
            Assert.AreEqual(jsonDosServiceDetailsByIdResponse, DosServiceDetailsByIdResponse);
        }


        private string ReadJsonFile(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd().Replace("\r\n", "").Replace(" ", "");
            }
        }
    }
}