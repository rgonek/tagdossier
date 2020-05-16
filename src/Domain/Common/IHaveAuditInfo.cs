using TagDossier.Domain.Entities;
using TagDossier.Domain.ValueObjects;

namespace TagDossier.Domain.Common
{
    public interface IHaveAuditInfo
    {
        AuditInfo Created { get; }
        AuditInfo LastModified { get; }

        void SetCreatedBy(ApplicationUser user);
        void SetModifiedBy(ApplicationUser user);
    }
}