using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? Status { get; set; }
        public ICollection<AppUserRole>? AppRoles { get; set; }
        public Person? Person { get; set; }
    }
}
