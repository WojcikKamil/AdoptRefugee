using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string? GetUserName(this ClaimsPrincipal userName)
        {
            return userName.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
