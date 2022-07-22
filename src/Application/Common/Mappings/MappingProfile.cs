using Application.TestTemplates.Queries.GetTestTemplateById;
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
        CreateMap<TestResult, TestResultDto>();
    }
}