using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TagDossier.Application.Common;
using TagDossier.Domain.ValueObjects;

namespace TagDossier.Application.Tags.Commands.ChangeColor
{
    public class ChangeColorCommand : IRequest<Unit>
    {
        public int Id { get; }
        public Color Color { get; }

        public ChangeColorCommand(int id, Color color)
        {
            Id = id;
            Color = color;
        }
    }

    public class ChangeColorCommandHandler : IRequestHandler<ChangeColorCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public ChangeColorCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<Unit> Handle(ChangeColorCommand request, CancellationToken cancellationToken)
        {
            var tag = await _db.Tags.GetAsync(request.Id, cancellationToken);
            tag.SetColor(request.Color);

            await _db.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}