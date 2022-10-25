using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Holding.Base.Security.SecurityModels
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [MaxLength(10)]
        public string NationalCode { get; set; }
    }
}