using System;
using AutoMapper;
using DirectoryOfServices;
using NHS111.Domain.Dos.Api.Models.Request;
using NHS111.Domain.Dos.Api.Models.Response;
using AgeFormatType = NHS111.Domain.Dos.Api.Models.Request.AgeFormatType;
using ServiceCareItemRotaSession = NHS111.Domain.Dos.Api.Models.Response.ServiceCareItemRotaSession;
using TimeOfDay = NHS111.Domain.Dos.Api.Models.Response.TimeOfDay;
using ServiceType = NHS111.Domain.Dos.Api.Models.Response.ServiceType;

namespace NHS111.Domain.Dos.Api.Models.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DosCheckCapacitySummaryRequest, CheckCapacitySummaryRequest>()
                .ForMember(dest => dest.c, opt => opt.MapFrom(src => src.Case));
            CreateMap<DosCase, Case>()
                .ForMember(dest => dest.forceSearchDistance, opt => opt.Ignore())
                .ForMember(dest => dest.ageFormat, opt => opt.ConvertUsing(new FromAgeFormatToAgeFormatConvertor()));

            CreateMap<DosServiceDetailsByIdRequest, ServiceDetailsByIdRequest>();

            CreateMap<CheckCapacitySummaryResponse, DosCheckCapacitySummaryResponse>()
                .ForMember(dest => dest.CheckCapacitySummaryResult, opt => opt.MapFrom(src => src.CheckCapacitySummaryResult));
            CreateMap<ServiceCareSummaryDestination, DosService>()
                .ForMember(dest => dest.ReferralText, opt => opt.MapFrom(src => src.publicFacingInformation))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.referralInformation))
                .ForMember(dest => dest.RotaSessionsAndSpecifiedSessions, opt => opt.Ignore());

            CreateMap<ServiceDetailsByIdResponse, DosServiceDetailsByIdResponse>();
            CreateMap<ServiceDetail, ServiceDetails>()
                .ForMember(dest => dest.ContactDetails, opt => opt.MapFrom(src => src.serviceEndpoints));
            CreateMap<Endpoint, ContactDetails>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => $"{src.address}\\|{src.interaction}\\|{src.format}\\|{src.businessScenario}\\|{src.comment}\\|{src.compression}"))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.endpointOrder))
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.transport))
                .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
    }

    public class FromAgeFormatToAgeFormatConvertor : IValueConverter<AgeFormatType, DirectoryOfServices.AgeFormatType>
    {
        public DirectoryOfServices.AgeFormatType Convert(AgeFormatType sourceMember, ResolutionContext context)
        {
            switch (sourceMember)
            {
                case AgeFormatType.Days:
                    return DirectoryOfServices.AgeFormatType.Days;
                case AgeFormatType.Months:
                    return DirectoryOfServices.AgeFormatType.Months;
                case AgeFormatType.Years:
                    return DirectoryOfServices.AgeFormatType.Years;
                case AgeFormatType.AgeGroup:
                    return DirectoryOfServices.AgeFormatType.AgeGroup;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sourceMember), sourceMember, null);
            }
        }
    }
}
