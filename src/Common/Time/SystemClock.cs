using System;

namespace TagDossier.Common.Time
{
    public class SystemClock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}