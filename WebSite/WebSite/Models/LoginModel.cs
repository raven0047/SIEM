using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { set; get; }
        [Required]
        public string Password { set; get; }
    }
}
