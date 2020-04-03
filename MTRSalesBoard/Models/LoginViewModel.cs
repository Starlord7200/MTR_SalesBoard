using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class LoginViewModel
    {
        [Required]
        [UIHint("username")]
        public string UserName { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
