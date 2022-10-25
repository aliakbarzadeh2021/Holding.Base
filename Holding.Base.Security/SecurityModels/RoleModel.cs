using System.ComponentModel.DataAnnotations;

namespace Holding.Base.Security.SecurityModels
{
    public class RoleModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }
    }
}
