using Dawn;
using TagDossier.Domain.Common;

namespace TagDossier.Domain.Entities
{
    public class Dossier : AuditableEntity<long>
    {
        public Resource Resource { get; }
        public Tag Tag { get; }

        private Dossier()
        {
        }

        public Dossier(Resource resource, Tag tag)
        {
            Guard.Argument(resource, nameof(resource)).NotNull();
            Guard.Argument(tag, nameof(tag)).NotNull();

            Resource = resource;
            Tag = tag;
        }
    }
}