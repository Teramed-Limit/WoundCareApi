using AutoMapper;
using WoundCareApi.Application.DTOs;
using WoundCareApi.Core.Domain.Entities;

namespace WoundCareApi.Infrastructure.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CRS_Case, CaseDto>()
            .ForMember(
                dest => dest.CaseTypeShortLabel,
                opt => opt.Ignore() // 忽略這個屬性，因為它來自 join
            );

        CreateMap<object, CaseRecordDto>()
            .ForMember(dest => dest.FormDefine, opt => opt.Ignore())
            .ForMember(dest => dest.FormData, opt => opt.Ignore());
    }
}
