using System.Security.Claims;

namespace MovieApp.Business.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsInRoles(this ClaimsPrincipal claimsPrincipal, string[] roles)
        {
            foreach (var item in roles)
            {
                if (claimsPrincipal.IsInRole(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
