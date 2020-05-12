using System.Security.Claims;
using TagDossier.Domain.Entities;

namespace TagDossier.Application
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal Claims { get; }
        ApplicationUser User { get; }
    }
}