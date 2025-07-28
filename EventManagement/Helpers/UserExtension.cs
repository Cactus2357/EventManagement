using System.Security.Claims;

namespace EventManagement.Helpers
{
    public static class UserExtension
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole("admin");
        }

        public static bool IsOrganizer(this ClaimsPrincipal user)
        {
            return user.IsInRole("organizer");
        }

        public static bool IsEventOwner(this ClaimsPrincipal user, int organizerId)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            return user.IsInRole("organizer") && userIdClaim?.Value == organizerId.ToString();
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
