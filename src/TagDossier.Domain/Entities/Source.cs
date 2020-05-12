using Dawn;
using TagDossier.Domain.Common;

namespace TagDossier.Domain.Entities
{
    public class Source : AuditableEntity<int>
    {
        public Connector Connector { get; }
        public string Token { get; private set; }

        public Source(Connector connector, string token)
        {
            Guard.Argument(connector, nameof(connector)).NotNull();

            Connector = connector;
            SetToken(token);
        }

        public void SetToken(string token)
        {
            Guard.Argument(token, nameof(token)).NotNull().NotWhiteSpace();

            Token = token;
        }
    }
}