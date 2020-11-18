using System.Threading.Tasks;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests.TestStartup
{
    public abstract class IntegrationStartupTestBase : IAsyncLifetime
    {
        public virtual async Task InitializeAsync()
        {
            await StartupTestBase.ResetCheckpoint().ConfigureAwait(false);
        }

        public virtual Task DisposeAsync() => Task.CompletedTask;
    }
}
