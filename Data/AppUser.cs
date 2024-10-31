using Microsoft.AspNetCore.Identity;

namespace BaseWebApplication.Data
{
    public class AppUser : IdentityUser
    {
        public string PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }

    }
}
