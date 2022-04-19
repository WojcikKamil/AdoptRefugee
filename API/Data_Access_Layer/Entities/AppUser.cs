using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? Status { get; set; }
        public ICollection<AppUserRole>? AppRoles { get; set; }
        public Refugee? Refugee { get; set; }
        public Benefactor? Benefactor { get; set; }
        public Accommodation ?Accommodation { get; set; }
    }
}
