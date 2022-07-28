using Application.Common.Interfaces;
using Application.TestTemplates.Queries;
using Infrastructure.Emails;
using Infrastructure.Persistence;
using Infrastructure.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("PsychologiesDb"));

        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

        return services;
    }
}