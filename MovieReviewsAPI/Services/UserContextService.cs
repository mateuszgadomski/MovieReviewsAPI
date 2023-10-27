using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MovieReviewsAPI.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
        public string? GetRoleName { get; }
        public bool IsAdministrator { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _contextAccessor.HttpContext?.User;

        public int? GetUserId =>
            User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public string? GetRoleName =>
            User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.Role).Value.ToString();

        public bool IsAdministrator => GetRoleName == "Administrator";
    }
}