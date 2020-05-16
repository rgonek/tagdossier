using System.Collections.Generic;
using Dawn;
using TagDossier.Domain.Common;

namespace TagDossier.Domain.Entities
{
    public class Resource : AuditableEntity<long>
    {
        public Source Source { get; }
        public string ExternalId { get; }

        private readonly List<Dossier> _dossiers = new List<Dossier>();
        public IReadOnlyCollection<Dossier> Dossiers => _dossiers.AsReadOnly();

        private Resource()
        {
        }

        public Resource(Source source, string externalId)
        {
            Guard.Argument(source, nameof(source)).NotNull();
            Guard.Argument(externalId, nameof(externalId)).NotNull().NotWhiteSpace();

            Source = source;
            ExternalId = externalId;
        }

        public void AddTag(Tag tag)
        {
            Guard.Argument(tag, nameof(tag)).NotNull();

            _dossiers.Add(new Dossier(this, tag));
        }
    }
}