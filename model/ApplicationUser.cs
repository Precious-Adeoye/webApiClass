using Microsoft.AspNetCore.Identity;

namespace webApiClass.model
{
    public class ApplicationUser : IdentityUser 
    {
        public string fullName { get; set; }
    }
}
