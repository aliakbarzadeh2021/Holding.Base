using System.ComponentModel.DataAnnotations;

namespace Holding.Base.Security.SecurityModels
{
    public class ChangeUserNameModel
    {
        [Required]
        [Display(Name = "Current UserName")]
        public string CurrentUserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "New RoleName")]
        public string NewUserName { get; set; }
    }
}
