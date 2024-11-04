using Microsoft.AspNetCore.Identity;

namespace BaseWebApplication.Data
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? MiddleLastName { get; set; }

    }
}
