using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class LoginViewModel
    {
        // View model maintaining the validation for the inputs from the login page
        #region Properties
        [Required]
        [UIHint("Username")]
        public string Username { get; set; }
        [Required]
        [UIHint("Password")]
        public string Password { get; set; }
        #endregion
    }
}
