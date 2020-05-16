using System;
using System.Collections.Generic;
using TagDossier.Common.Time;
using TagDossier.Domain.Common;
using TagDossier.Domain.Entities;

namespace TagDossier.Domain.ValueObjects
{
    public class AuditInfo : ValueObject
    {
        public DateTime On { get; private set; }
        public ApplicationUser By { get; private set; }
        
        private AuditInfo() { }

        public static AuditInfo For(ApplicationUser user)
        {
            return new AuditInfo
            {
                By = user,
                On = Clock.UtcNow
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return On;
            yield return By;
        }
    }
}