using System.ComponentModel.DataAnnotations;

namespace BaseWebApplication.Models
{
    public class AppUserVM
    {
        [Display(Name = "User name")]
        public string? UserName { get; set; }
        [Display(Name = "Email confirmed")]
        public bool EmailConfirmed { get; set; }
        [Display(Name = "Last name")]
        public string PrimerApellido { get; set; }
        [Display(Name = "First name")]
        public string PrimerNombre { get; set; }
        [Display(Name = "Middle last name")]
        public string? SegundoApellido { get; set; }
        [Display(Name = "Middle name")]

        public string? SegundoNombre { get; set; }
    }
}
