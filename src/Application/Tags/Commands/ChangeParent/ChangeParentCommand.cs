using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TagDossier.Application.Common;

namespace TagDossier.Application.Tags.Commands.ChangeParent
{
    public class ChangeParentCommand : IRequest<Unit>
    {
        public int Id { get; }
        public int ParentId { get; }

        public ChangeParentCommand(int id, int parentId)
        {
            Id = id;
            ParentId = parentId;
        }
    }

    public class ChangeParentCommandHandler : IRequestHandler<ChangeParentCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public ChangeParentCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<Unit> Handle(ChangeParentCommand request, CancellationToken cancellationToken)
        {
            var tag = await _db.Tags.GetAsync(request.Id, cancellationToken);
            var parentTag = await _db.Tags.GetAsync(request.ParentId, cancellationToken);

            tag.SetParent(parentTag);

            await _db.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}