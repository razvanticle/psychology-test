using System.Reflection;
using Application.Common.Behaviours;
using Application.Tests.Commands.ComputeTestResult.ScoreCalculator;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddScoped(typeof(IScoreCalculator<IEnumerable<WeightedScoreInput>>), typeof(WeightedSumScoreCalculator));

        return services;
    }
}