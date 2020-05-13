using System.Linq;
using Dawn;
using TagDossier.Domain.Common;

namespace TagDossier.Domain.Entities
{
    public class Connector : Entity<int>
    {
        public static readonly Connector Dropbox = new Connector(1, "Dropbox");
        public static readonly Connector GoogleDrive = new Connector(2, "GoogleDrive");
        public static readonly Connector OneDrive = new Connector(3, "OneDrive");

        public static readonly Connector[] AllConnectors = { Dropbox, GoogleDrive, OneDrive };

        public string Name { get; private set; }
        
        public Connector(string name)
        {
            SetName(name);
        }

        private Connector(int id, string name)
            : base(id)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            Guard.Argument(name, nameof(name)).NotNull().NotWhiteSpace();

            Name = name;
        }

        public static Connector FromId(int id)
        {
            return AllConnectors.SingleOrDefault(x => x.Id == id);
        }
    }
}