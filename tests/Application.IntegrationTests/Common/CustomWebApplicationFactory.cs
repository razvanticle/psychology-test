using System.Linq;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Common;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((builder, services) =>
        {
            var serviceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IDatabaseSeeder));

            if (serviceDescriptor != null)
            {
                services.Remove(serviceDescriptor);
                services.AddScoped<IDatabaseSeeder, TestDataBaseSeeder>();
            }
        });
    }
}