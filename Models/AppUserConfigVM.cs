using System.ComponentModel.DataAnnotations;

namespace BaseWebApplication.Models
{
    public class AppUserConfigVM : BaseIdentityVM<int>
    {
        public override int ID { get; set; }
        [Display(Name = "Creation date")]
        public DateTime CreateDate { get; set; }

        #region Calculate Fields

        [Display(Name = "Names")]
        public string Names => AppUser == null ? "" : AppUser.FirstName + " " + AppUser.MiddleName;
        [Display(Name = "Surnames")]
        public string Surnames => AppUser == null ? "" : AppUser.LastName + " " + AppUser.MiddleLastName;
        #endregion

        public AppUserVM AppUser { get; set; }
    }
}
