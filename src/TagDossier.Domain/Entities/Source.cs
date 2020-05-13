using Dawn;
using TagDossier.Domain.Common;

namespace TagDossier.Domain.Entities
{
    public class Source : AuditableEntity<int>
    {
        public Connector Connector { get; }

        private Source()
        {
        }

        public Source(Connector connector)
        {
            Guard.Argument(connector, nameof(connector)).NotNull();

            Connector = connector;
        }
    }
}