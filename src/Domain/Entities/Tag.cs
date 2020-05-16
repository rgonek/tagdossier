using Dawn;
using TagDossier.Domain.Common;
using TagDossier.Domain.ValueObjects;

namespace TagDossier.Domain.Entities
{
    public class Tag : AuditableEntity<int>
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }
        public Tag Parent { get; private set; }

        private Tag()
        {
        }

        public Tag(string name, Color color, Tag parent = null)
        {
            SetName(name);
            SetColor(color);
            SetParent(parent);
        }

        public void SetName(string name)
        {
            Guard.Argument(name, nameof(name)).NotNull().NotWhiteSpace();

            Name = name;
        }

        public void SetColor(Color color)
        {
            Guard.Argument(color, nameof(color)).NotNull();

            Color = color;
        }

        public void SetParent(Tag parent)
        {
            Parent = parent;
        }
    }
}