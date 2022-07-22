using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Application.IntegrationTests.Common;

public class BaseTests
{
    private CustomWebApplicationFactory applicationFactory;
    private IServiceScopeFactory scopeFactory;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        applicationFactory = new CustomWebApplicationFactory();
        scopeFactory = applicationFactory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    protected Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request)
    {
        using var scope = scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return mediator.Send(request);
    }

    protected TDestination Map<TSource, TDestination>(TSource source)
    {
        using var scope = scopeFactory.CreateScope();

        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        return mapper.Map<TSource, TDestination>(source);
    }
}