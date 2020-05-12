using System.Threading.Tasks;
using TagDossier.IntegrationTests.Infrastructure;
using Xunit;

namespace TagDossier.IntegrationTests
{
    using static DatabaseFixture;

    [Collection(nameof(SharedFixture))]
    public class Test
    {
        [Fact]
        public async Task test()
        {
        }
    }
}