using BaseWebApplication.Resources;
using System.ComponentModel.DataAnnotations;

namespace BaseWebApplication.Models
{
    public class AppUserVM
    {
        [Display(Name = nameof(Resource.AppUser_UserName), ResourceType = typeof(Resource))]
        public string? UserName { get; set; }

        [Display(Name = nameof(Resource.AppUser_EmailConfirmed), ResourceType = typeof(Resource))]
        public bool EmailConfirmed { get; set; }

        [Display(Name = nameof(Resource.AppUser_FirstName), ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessageResourceName = nameof(Resource.General_StringLength_Error), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
        public string FirstName { get; set; }

        [Display(Name = nameof(Resource.AppUser_MiddleLastName), ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessageResourceName = nameof(Resource.General_StringLength_Error), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
        public string? MiddleLastName { get; set; }

        [Display(Name = nameof(Resource.AppUser_LastName), ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessageResourceName = nameof(Resource.General_StringLength_Error), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
        public string LastName { get; set; }

        [Display(Name = nameof(Resource.AppUser_MiddleName), ResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessageResourceName = nameof(Resource.General_StringLength_Error), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
        public string? MiddleName { get; set; }
    }
}
