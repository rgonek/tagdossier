using System;
using System.Security.Claims;

namespace TagDossier.Application.Common
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
            => Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        public static string GetUserName(this ClaimsPrincipal principal)
            => principal.FindFirst(ClaimTypes.Name)?.Value;
    }
}