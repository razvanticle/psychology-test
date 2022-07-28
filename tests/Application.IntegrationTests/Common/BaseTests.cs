using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Common;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Application.IntegrationTests.Common;

public class BaseTests
{
    private CustomWebApplicationFactory? applicationFactory;
    private IServiceScopeFactory? scopeFactory;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        applicationFactory = new CustomWebApplicationFactory();
        scopeFactory = applicationFactory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        ClearDatabase();
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
    
    protected async Task<TEntity?> GetEntityById<TEntity>(int id) where TEntity : BaseEntity
    {
        using var scope = scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IRepository>();

        return await repository.GetEntities<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    protected bool ClearDatabase()
    {
        using var scope = scopeFactory.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return context.Database.EnsureDeleted();
    }
}