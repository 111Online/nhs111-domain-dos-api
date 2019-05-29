using AutoMapper;
using NHS111.Domain.Dos.Api.Models.Mappers;
using NHS111.Domain.Dos.Api.Models.Request;
using NUnit.Framework;
using PathwayService;
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
            Assert.AreEqual(PathwayService.AgeFormatType.Years, result.c.ageFormat);
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
