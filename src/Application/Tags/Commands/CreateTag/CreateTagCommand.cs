using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TagDossier.Domain.Entities;
using TagDossier.Domain.ValueObjects;

namespace TagDossier.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommand : IRequest<int>
    {
        public string Name { get; }

        public CreateTagCommand(string name)
        {
            Name = name;
        }
    }

    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
    {
        private readonly IApplicationDbContext _db;

        public CreateTagCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = new Tag(request.Name, Color.Default);

            _db.Tags.Add(tag);

            await _db.SaveChangesAsync(cancellationToken);

            return tag.Id;
        }
    }
}