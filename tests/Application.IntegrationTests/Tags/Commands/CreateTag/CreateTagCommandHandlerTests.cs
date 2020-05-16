using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using TagDossier.Application.Tags.Commands.CreateTag;
using TagDossier.CommonTests.Infrastructure;
using TagDossier.Domain.Entities;
using TagDossier.IntegrationTests.Infrastructure;
using Xunit;

namespace TagDossier.Application.IntegrationTests.Tags.Commands.CreateTag
{
    using static TestFixture;
    using static DatabaseFixture;

    [Collection(nameof(SharedFixture))]
    public class CreateTagCommandHandlerTests
    {
        [Fact]
        public async Task should_create_tag()
        {
            await RunAsNewUserAsync();
            var command = F.Create<CreateTagCommand>();

            var tagId = await SendAsync(command);

            var category = await FindAsync<Tag>(tagId);
            category.Name.Should().Be(command.Name);
        }
    }
}