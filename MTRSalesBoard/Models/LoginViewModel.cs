using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class LoginViewModel
    {
        // View model maintaining the validation for the inputs from the login page
        #region Properties
        [Required]
        [UIHint("username")]
        public string Username { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        #endregion
    }
}
