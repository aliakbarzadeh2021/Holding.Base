using System.ComponentModel.DataAnnotations;
using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.Security.SecurityModels
{
    public class UserModel
    {
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "طول کلمه عبور باید بین {0} و {1} باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = " تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور تطابق تدارد")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "نقش")]
        public UserRole Role { get; set; }
    }
}