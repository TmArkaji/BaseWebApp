using Microsoft.AspNetCore.Identity;

namespace BaseWebApplication.Data
{
    public class AppUser : IdentityUser
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string primerNombre { get; set; }
        public string? segundoNombre { get; set; }
        public string primerApellido { get; set; }
        public string? segundoApellido { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}
