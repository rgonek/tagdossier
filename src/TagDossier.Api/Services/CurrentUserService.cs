using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TagDossier.Application;
using TagDossier.Application.Common;
using TagDossier.Domain.Entities;

namespace TagDossier.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public ClaimsPrincipal Claims => _accessor.HttpContext.User;
        public ApplicationUser User => ApplicationUser.FromId(Claims.GetUserId());
    }
}