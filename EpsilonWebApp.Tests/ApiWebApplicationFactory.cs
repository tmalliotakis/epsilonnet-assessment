using EpsilonWebApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EpsilonWebApp.Tests
{
    /// <summary>
    /// Boots the real application for integration testing, but under the "Testing"
    /// environment (which skips the SQL Server startup migration) and with the
    /// SQL Server DbContext swapped for an in-memory database so the tests are
    /// self-contained and do not require Docker.
    /// </summary>
    public class ApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Remove every EF Core registration tied to the SQL Server provider.
                // In EF Core 9 AddDbContext also registers IDbContextOptionsConfiguration<T>,
                // which is what actually applies the provider - leaving it in place makes EF
                // see both SQL Server and InMemory providers and throw at first query.
                var toRemove = services.Where(d =>
                    d.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
                    d.ServiceType == typeof(DbContextOptions) ||
                    d.ServiceType == typeof(AppDbContext) ||
                    (d.ServiceType.IsGenericType &&
                     d.ServiceType.GetGenericTypeDefinition().Name.StartsWith("IDbContextOptionsConfiguration")))
                    .ToList();

                foreach (var descriptor in toRemove)
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("EpsilonApiTests"));
            });
        }
    }
}
