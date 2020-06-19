using System.ComponentModel.DataAnnotations;

namespace MTRSalesBoard.Models
{
    public class RegisterUserViewModel
    {
        // This viewmodel maintains the validation for the inputs from the register page
        #region Properties
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"(^[A-Z]|[a-z])([A-Z]*[a-z]*[0-9]*)*$", ErrorMessage = "Can Not Begin With A Number. No Spaces. No Special Characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[A-Z][a-z]*((\s)?[A-Z][a-z]*)*$", ErrorMessage = "First Name. Optional Last Name. No Numbers.")]
        [UIHint("Full Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Not a valid email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Passwords must contain one uppercase, one lowercase, one number and one special character")]
        [StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }
        #endregion
    }
}
