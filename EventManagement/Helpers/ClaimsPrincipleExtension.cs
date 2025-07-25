using System.Security.Claims;

namespace EventManagement.Helpers
{
    public static class ClaimsPrincipleExtension
    {
        public static bool IsInAnyRole(this ClaimsPrincipal user, params string[] roles)
        {
            return roles.Any(user.IsInRole);
        }
    }
}
