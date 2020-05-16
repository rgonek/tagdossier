using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TagDossier.Domain.Entities;

namespace TagDossier.Application.Tags.Queries.GetTags
{
    public class GetTagsQuery : IRequest<IEnumerable<TagVm>>
    {
    }

    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, IEnumerable<TagVm>>
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentUserService _currentUserService;

        public GetTagsQueryHandler(IApplicationDbContext db, ICurrentUserService currentUserService)
            => (_db, _currentUserService) = (db, currentUserService);

        public async Task<IEnumerable<TagVm>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _db.Tags
                .Where(x => x.Created.By.Id == _currentUserService.User.Id)
                .ToListAsync(cancellationToken);

            return GetTagsHierarchy(tags);
        }

        private static IList<TagVm> GetTagsHierarchy(IList<Tag> tags, Tag parent = null)
        {
            var tagsVm = new List<TagVm>();
            foreach (var tag in tags.Where(x => x.Parent == parent))
            {
                tagsVm.Add(new TagVm(tag, GetTagsHierarchy(tags, tag)));
            }

            return tagsVm;
        }
    }
}