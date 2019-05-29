using System;
using AutoMapper;
using NHS111.Domain.Dos.Api.Models.Request;
using PathwayService;
using AgeFormatType = NHS111.Domain.Dos.Api.Models.Request.AgeFormatType;

namespace NHS111.Domain.Dos.Api.Models.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DosCheckCapacitySummaryRequest, CheckCapacitySummaryRequest>()
                .ForMember(dest => dest.c, opt => opt.MapFrom(src => src.Case));
            CreateMap<DosServiceDetailsByIdRequest, ServiceDetailsByIdRequest>();
            CreateMap<DosCase, Case>()
                .ForMember(dest => dest.forceSearchDistance, opt => opt.Ignore())
                .ForMember(dest => dest.ageFormat, opt => opt.ConvertUsing(new FromAgeFormatToAgeFormatConvertor()));
        }
    }

    public class FromAgeFormatToAgeFormatConvertor : IValueConverter<AgeFormatType, PathwayService.AgeFormatType>
    {
        public PathwayService.AgeFormatType Convert(AgeFormatType sourceMember, ResolutionContext context)
        {
            switch (sourceMember)
            {
                case AgeFormatType.Days:
                    return PathwayService.AgeFormatType.Days;
                case AgeFormatType.Months:
                    return PathwayService.AgeFormatType.Months;
                case AgeFormatType.Years:
                    return PathwayService.AgeFormatType.Years;
                case AgeFormatType.AgeGroup:
                    return PathwayService.AgeFormatType.AgeGroup;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sourceMember), sourceMember, null);
            }
        }
    }
}
