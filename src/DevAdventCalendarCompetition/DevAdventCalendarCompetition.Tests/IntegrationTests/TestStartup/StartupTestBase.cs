using System;
using System.IO;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests.TestStartup
{
    public static class StartupTestBase
    {
        private static readonly IServiceScopeFactory ScopeFactory;

        private static readonly IConfiguration Configuration;

        private static readonly Checkpoint Checkpoint;

#pragma warning disable CA1810 // Initialize reference type static fields inline
        static StartupTestBase()
#pragma warning restore CA1810 // Initialize reference type static fields inline
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: false, true)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var startup = new Startup(Configuration);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            ScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
            Checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };
        }

        public static Task ResetCheckpoint() => Checkpoint.Reset(Configuration.GetConnectionString("DefaultConnection"));

        public static async Task AddApplicationUserAsync(ApplicationUser user)
        {
            using var scope = ScopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            await userManager.CreateAsync(user).ConfigureAwait(false);
        }

        public static Task<TResult> ExecuteAsync<TService, TResult>(Func<TService, Task<TResult>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<TService>()));

        private static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using var scope = ScopeFactory.CreateScope();
            return await action(scope.ServiceProvider).ConfigureAwait(false);
        }
    }
}
