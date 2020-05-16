using System;

namespace TagDossier.Common.Time
{
    public interface IClock
    {
        DateTime UtcNow { get; }
        DateTime Now { get; }
    }
}