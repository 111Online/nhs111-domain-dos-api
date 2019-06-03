﻿using System.Linq;
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
            var dosCheckCapacitySummaryRequest = new DosCheckCapacitySummaryRequest("madeUpUser", "madeUpPassword",
                new DosCase
                {
                    AgeFormat = AgeFormatType.Years
                });
            var result = Mapper.Map<DosCheckCapacitySummaryRequest, CheckCapacitySummaryRequest>(dosCheckCapacitySummaryRequest);
            Assert.AreEqual(DirectoryOfServices.AgeFormatType.Years, result.c.ageFormat);
        }

        [Test]
        public void FromDosServiceDetailsByIdRequestToServiceDetailsByIdRequest_IsValid()
        {
            var dosServiceDetailsByIdRequest = new DosServiceDetailsByIdRequest("madeUpUser", "madeUpPassword", "1234567890");
            var result = Mapper.Map<DosServiceDetailsByIdRequest, ServiceDetailsByIdRequest>(dosServiceDetailsByIdRequest);
            Assert.IsNotNull(result);
        }

        [Test]
        public void FromCheckCapacitySummaryResponseToDosCheckCapacitySummaryResponse_IsValid()
        {
            var checkCapacitySummaryResponse = new CheckCapacitySummaryResponse("1234", "2019-05-29T15:38:36",
                "2019-05-29T15:38:36", 8035.5f, 30, DistanceSource.National,
                new[]
                {
                    new ServiceCareSummaryDestination
                    {
                        publicFacingInformation = "some referral text",
                        referralInformation = "some notes about this service"
                    },
                });

            var result = Mapper.Map<CheckCapacitySummaryResponse, DosCheckCapacitySummaryResponse>(checkCapacitySummaryResponse);
            var firstService = result.CheckCapacitySummaryResult.First();
            Assert.IsNotNull(firstService);
            Assert.AreEqual(checkCapacitySummaryResponse.CheckCapacitySummaryResult.First().referralInformation, firstService.Notes);
            Assert.AreEqual(checkCapacitySummaryResponse.CheckCapacitySummaryResult.First().publicFacingInformation, firstService.ReferralText);
            Assert.IsNotNull(firstService.RotaSessions);
        }

        [Test]
        public void FromServiceDetailsByIdResponseToDosServiceDetailsByIdResponse_IsValid()
        {
            var serviceDetailsByIdResponse = new ServiceDetailsByIdResponse(new[]
            {
                new ServiceDetail
                {
                    id = "123455",
                    odsCode = "UNK",
                    serviceEndpoints = new[] { new Endpoint { address = "http://someaddress.com", endpointOrder = 1, transport = "itk" } }
                },
            });

            var result = Mapper.Map<ServiceDetailsByIdResponse, DosServiceDetailsByIdResponse>(serviceDetailsByIdResponse);
            Assert.IsNotNull(serviceDetailsByIdResponse.services[0]);
            Assert.IsNotNull(serviceDetailsByIdResponse.services[0].serviceEndpoints[0]);
            Assert.AreEqual(serviceDetailsByIdResponse.services[0].serviceEndpoints[0].address, result.Services[0].ContactDetails[0].Value);
            Assert.AreEqual(serviceDetailsByIdResponse.services[0].serviceEndpoints[0].endpointOrder, result.Services[0].ContactDetails[0].Order);
            Assert.AreEqual(ContactType.itk, result.Services[0].ContactDetails[0].Tag);
        }
    }
}