using Application.Common.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TestTemplate, TestTemplateDto>();
        CreateMap<TestQuestion, TestQuestionDto>();
        CreateMap<TestAnswer, TestAnswerDto>();
        CreateMap<PossibleTestResult, TestResultDto>();
        CreateMap<TestResult, TestResultDto>()
            .ForMember(dest => dest.TestName, opt => opt.MapFrom(src => src.TestTemplate.Title));
    }
}