using TagDossier.Domain.Entities;
using TagDossier.Domain.ValueObjects;

namespace TagDossier.Domain.Common
{
    public class AuditableEntity<T> : Entity<T>, IHaveAuditInfo
    {
        public AuditInfo Created { get; private set; }
        public AuditInfo LastModified { get; private set; }

        protected AuditableEntity()
        {
        }

        protected AuditableEntity(T id)
            : base(id)
        {
        }

        public void SetCreatedBy(ApplicationUser createdBy)
        {
            if (Created != null)
            {
                //throw
            }

            Created = AuditInfo.For(createdBy);
        }

        public void SetModifiedBy(ApplicationUser modifiedBy)
        {
            if (LastModified == null)
            {
                //throw
            }

            LastModified = AuditInfo.For(modifiedBy);
        }
    }
}