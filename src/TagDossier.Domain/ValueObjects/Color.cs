using System.Collections.Generic;
using Dawn;
using TagDossier.Domain.Common;

namespace TagDossier.Domain.ValueObjects
{
    public class Color : ValueObject
    {
        public string Text { get; }
        public string Background { get; }

        public Color(string text, string background)
        {
            Guard.Argument(text, nameof(text)).NotNull().NotWhiteSpace().Length(6);
            Guard.Argument(background, nameof(background)).NotNull().NotWhiteSpace().Length(6);

            Text = text;
            Background = background;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Text;
            yield return Background;
        }
    }
}