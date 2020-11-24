using System;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests.TestStartup
{
    public class StartupTestBase
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly IConfiguration _configuration;

        protected StartupTestBase()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, true)
                .AddJsonFile("appsettings.Local.json", false, true)
                .AddEnvironmentVariables();

            this._configuration = builder.Build();
            var startup = new Startup(this._configuration);
            var services = new ServiceCollection();
            var databaseName = Guid.NewGuid().ToString();
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(databaseName));
            startup.ConfigureServices(services);
            this._scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        protected async Task AddApplicationUserAsync(ApplicationUser user)
        {
            using var scope = this._scopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            await userManager.CreateAsync(user).ConfigureAwait(false);
        }

        protected Task<TResult> ExecuteAsync<TService, TResult>(Func<TService, Task<TResult>> action)
            => this.ExecuteScopeAsync(sp => action(sp.GetService<TService>()));

        private async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using var scope = this._scopeFactory.CreateScope();
            return await action(scope.ServiceProvider).ConfigureAwait(false);
        }
    }
}
