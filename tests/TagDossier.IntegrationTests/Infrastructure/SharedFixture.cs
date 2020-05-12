using System.Threading.Tasks;
using Nito.AsyncEx;
using Xunit;

namespace TagDossier.IntegrationTests.Infrastructure
{
    using static DatabaseFixture;
    
    public class SharedFixture  : IAsyncLifetime
    {
        private static readonly AsyncLock Mutex = new AsyncLock();

        private static bool _initialized;

        public virtual async Task InitializeAsync()
        {
            if (_initialized)
                return;

            using (await Mutex.LockAsync())
            {
                if (_initialized)
                    return;
                
                await ResetState();

                _initialized = true;
            }
        }

        public virtual Task DisposeAsync() => Task.CompletedTask;
    }
}