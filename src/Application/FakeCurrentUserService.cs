using System;
using System.Security.Claims;
using TagDossier.Domain.Entities;

namespace TagDossier.Application
{
    public class FakeCurrentUserService :ICurrentUserService
    {
        public ApplicationUser User { get; private set; }

        public ClaimsPrincipal Claims =>
            new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "test"),
                new Claim(ClaimTypes.NameIdentifier, User is null ? "" : User.Id.ToString())
            }, "mock"));
        
        public FakeCurrentUserService(){}

        public FakeCurrentUserService(Guid userId)
        {
            User = ApplicationUser.FromId(userId);
        }

        public void SetCurrentUser(ApplicationUser user)
        {
            User = user == null ? null : ApplicationUser.FromId(user.Id);
        }

        public void Reset()
        {
            User = null;
        }
    }
}