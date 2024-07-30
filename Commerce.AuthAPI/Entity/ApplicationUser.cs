using Microsoft.AspNetCore.Identity;

namespace Commerce.AuthAPI.Entity
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
