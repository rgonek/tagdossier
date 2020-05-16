using System;

namespace TagDossier.Common.Time
{
    public static class Clock
    {
        internal static IClock UnderlyingClock = new SystemClock();
        public static DateTime UtcNow => UnderlyingClock.UtcNow;
        public static DateTime Now => UnderlyingClock.Now;
    }
}