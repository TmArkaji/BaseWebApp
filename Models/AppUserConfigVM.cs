using System.ComponentModel.DataAnnotations;

namespace BaseWebApplication.Models
{
    public class AppUserConfigVM : BaseIdentityVM<int>
    {
        public override int ID { get; set; }
        [Display(Name = "Creation date")]
        public DateTime createDate { get; set; }

        #region Calculate Fields

        [Display(Name = "Names")]
        public string names => AppUser == null ? "" : AppUser.PrimerNombre + " " + AppUser.SegundoNombre;
        [Display(Name = "Surnames")]
        public string surnames => AppUser == null ? "" : AppUser.PrimerApellido + " " + AppUser.SegundoApellido;
        #endregion

        public AppUserVM AppUser { get; set; }
    }
}
