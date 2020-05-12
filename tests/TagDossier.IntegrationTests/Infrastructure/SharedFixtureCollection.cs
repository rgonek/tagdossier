using Xunit;

namespace TagDossier.IntegrationTests.Infrastructure
{
    [CollectionDefinition(nameof(SharedFixture))]
    public class SharedFixtureCollection  : ICollectionFixture<SharedFixture>
    {
    }
}