using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class RegisterUserViewModel
    {
        // This viewmodel maintains the validation for the inputs from the register page
        #region Properties
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Name must contain character letters. No digits allowed")]
        [UIHint("Full Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Not a valid email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Passwords must contain one uppercase, one lowercase, one number and one special character")]
        [StringLength(10, MinimumLength = 6)]
        public string Password { get; set; }
        #endregion
    }
}
