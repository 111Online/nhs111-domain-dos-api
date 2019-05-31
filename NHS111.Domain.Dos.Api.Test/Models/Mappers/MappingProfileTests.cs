using System.Linq;
using AutoMapper;
using DirectoryOfServices;
using NHS111.Domain.Dos.Api.Models.Mappers;
using NHS111.Domain.Dos.Api.Models.Request;
using NHS111.Domain.Dos.Api.Models.Response;
using NUnit.Framework;
using AgeFormatType = NHS111.Domain.Dos.Api.Models.Request.AgeFormatType;

namespace NHS111.Domain.Dos.Api.Test.Models.Mappers
{
    [TestFixture]
    public class MappingProfileTests
    {
        [OneTimeSetUp]
        public void InitializeMappingProfile()
        {
            Mapper.Initialize(cfg => 
                cfg.AddProfile(new MappingProfile())
            );
        }

        [Test]
        public void MappingProfile_Configuration_IsValid_Test()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void FromDosCheckCapacitySummaryRequestToCheckCapacitySummaryRequest_IsValid()
        {
            var result = Mapper.Map<DosCheckCapacitySummaryRequest, CheckCapacitySummaryRequest>(GenerateDosCheckCapacitySummaryRequestObject());
            Assert.AreEqual("123", result.c.caseId);
            Assert.AreEqual("so302un", result.c.postcode);
            Assert.AreEqual(33, result.c.age);
            Assert.AreEqual(DirectoryOfServices.AgeFormatType.Years, result.c.ageFormat);
            Assert.AreEqual("111", result.c.caseRef);
            Assert.AreEqual(1008, result.c.disposition);
            Assert.AreEqual(GenderType.M, result.c.gender);
            Assert.AreEqual("123545645", result.c.searchDateTime);
            Assert.AreEqual(60, result.c.searchDistance);
            Assert.AreEqual(1010, result.c.symptomDiscriminatorList[0]);
            Assert.AreEqual(1000, result.c.symptomGroup);
            Assert.AreEqual("madeUpUser", result.userInfo.username);
            Assert.AreEqual("madeUpPassword", result.userInfo.password);
            Assert.AreEqual("1.5", result.serviceVersion);
        }

        [Test]
        public void FromDosServiceDetailsByIdRequestToServiceDetailsByIdRequest_IsValid()
        {
            var result = Mapper.Map<DosServiceDetailsByIdRequest, ServiceDetailsByIdRequest>(GenerateDosServiceDetailsByIdRequestObject());
            Assert.AreEqual("1234567890", result.serviceId);
            Assert.AreEqual("madeUpUser", result.userInfo.username);
            Assert.AreEqual("madeUpPassword", result.userInfo.password);
            Assert.AreEqual("1.5", result.serviceVersion);
        }

        [Test]
        public void FromCheckCapacitySummaryResponseToDoSCheckCapacitySummaryResponse_IsValid()
        {
            var checkCapacitySummaryResponse = new CheckCapacitySummaryResponse("1234", "2019-05-29T15:38:36", "2019-05-29T15:38:36", 8035.5f, 30, DistanceSource.National, new ServiceCareSummaryDestination[] { new ServiceCareSummaryDestination() { publicFacingInformation = "some referral text", referralInformation = "some notes about this service" },  });

            var result = Mapper.Map<CheckCapacitySummaryResponse, DoSCheckCapacitySummaryResponse>(checkCapacitySummaryResponse);
            Assert.AreEqual(checkCapacitySummaryResponse.TransactionId, result.TransactionId);
            Assert.AreEqual(checkCapacitySummaryResponse.CheckCapacitySummaryResult.First().referralInformation, result.CheckCapacitySummaryResult.First().Notes);
            Assert.AreEqual(checkCapacitySummaryResponse.CheckCapacitySummaryResult.First().publicFacingInformation, result.CheckCapacitySummaryResult.First().ReferralText);
        }

        private CheckCapacitySummaryResponse GenerateCheckCapacitySummaryResponseObject()
        {
            throw new System.NotImplementedException();
        }

        private DosCheckCapacitySummaryRequest GenerateDosCheckCapacitySummaryRequestObject()
        {
            var dosCase = new DosCase
            {
                CaseId = "123",
                PostCode = "so302un",
                Age = "33",
                AgeFormat = AgeFormatType.Years,
                CaseRef = "111",
                Disposition = 1008,
                Gender = "M",
                SearchDateTime = "123545645",
                SearchDistance = 60,
                SymptomDiscriminatorList = new[] { 1010 },
                SymptomGroup = 1000
            };
            return new DosCheckCapacitySummaryRequest("madeUpUser", "madeUpPassword", dosCase);
        }

        private DosServiceDetailsByIdRequest GenerateDosServiceDetailsByIdRequestObject()
        {
            return new DosServiceDetailsByIdRequest("madeUpUser", "madeUpPassword", "1234567890");
        }
    }
}
