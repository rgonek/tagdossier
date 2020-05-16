using System;
using Dawn;
using Microsoft.AspNetCore.Identity;
using TagDossier.Domain.Common;

namespace TagDossier.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity<Guid>
    {
        [PersonalData]
        public string FirstName { get; private set; }

        private ApplicationUser() { }

        public ApplicationUser(string firstName)
        {
            SetFirstName(firstName);
        }

        public void SetFirstName(string firstName)
        {
            Guard.Argument(firstName, nameof(firstName)).NotNull().NotWhiteSpace();

            FirstName = firstName;
        }

        public static ApplicationUser FromId(Guid id)
        {
            return new ApplicationUser
            {
                Id = id
            };
        }

        #region Entity Equality

        public override bool Equals(object obj)
        {
            if (!(obj is ApplicationUser other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            if (Id == default || other.Id == default)
                return false;

            return Equals(Id, other.Id);
        }

        public static bool operator ==(ApplicationUser a, ApplicationUser b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ApplicationUser a, ApplicationUser b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType() + Id.ToString()).GetHashCode();
        }

        private Type GetRealType()
        {
            var type = GetType();

            return type.ToString().Contains("Castle.Proxies.") ? type.BaseType : type;
        }
        
        #endregion
    }
}