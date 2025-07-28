using EventManagement.Enums;
using System.Security.Claims;

namespace EventManagement.Helpers
{
    public static class UserExtension
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(UserRoles.Admin);
        }

        public static bool IsOrganizer(this ClaimsPrincipal user)
        {
            return user.IsInRole(UserRoles.Organizer);
        }

        public static bool IsEventOwner(this ClaimsPrincipal user, int organizerId)
        {
            return user.GetCurrentUserId() == organizerId;
        }

        public static bool IsAdminOrOrganizer(this ClaimsPrincipal user)
        {
            return user.IsAdmin() || user.IsOrganizer();
        }

        public static int GetCurrentUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }
    }
}
