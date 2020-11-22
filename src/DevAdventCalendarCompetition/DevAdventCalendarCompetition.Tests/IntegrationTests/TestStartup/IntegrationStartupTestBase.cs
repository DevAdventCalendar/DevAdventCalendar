using System.Threading.Tasks;
using Nito.AsyncEx;
using Xunit;

namespace DevAdventCalendarCompetition.Tests.IntegrationTests.TestStartup
{
    public abstract class IntegrationStartupTestBase : IAsyncLifetime
    {
        private static readonly AsyncLock Mutex = new AsyncLock();

        private static bool _initialized;

        public virtual async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }

            using (await Mutex.LockAsync())
            {
                if (_initialized)
                {
                    return;
                }

                await StartupTestBase.ResetCheckpoint().ConfigureAwait(false);

                _initialized = true;
            }
        }

        public virtual Task DisposeAsync() => Task.CompletedTask;
    }
}
