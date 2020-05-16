using System.Collections.Generic;
using TagDossier.Domain.Entities;
using TagDossier.Domain.ValueObjects;

namespace TagDossier.Application.Tags.Queries.GetTags
{
    public class TagVm
    {
        public string Name { get; }
        public Color Color { get; }
        public IList<TagVm> Children { get; }

        public TagVm(Tag tag, IList<TagVm> children)
        {
            Name = tag.Name;
            Color = tag.Color;
            Children = children ?? new List<TagVm>();
        }
    }
}