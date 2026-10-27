using System.Security.Claims;

namespace Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool TryGetIdFromAccessToken(this ClaimsPrincipal user, out Guid userId)
    {
        var userIdAsStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdAsStr) == false
            && Guid.TryParse(userIdAsStr, out userId))
        {
            return true;
        }

        userId = Guid.Empty;
        return false;
    }
}