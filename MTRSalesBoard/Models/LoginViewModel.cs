using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class LoginViewModel
    {
        [Required]
        [UIHint("username")]
        public string Username { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
