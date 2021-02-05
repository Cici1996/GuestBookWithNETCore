using GuestBook.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace GuestBook.WebApp.Models.Accounts
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = ModelMessageConstants.UsernameRequired)]
        [Display(Name = "Username")]
        [MaxLength(255, ErrorMessage = ModelMessageConstants.MaxLength255Chars)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ModelMessageConstants.PasswordRequired),
            MinLength(4, ErrorMessage = ModelMessageConstants.MinLength4Chars),
            DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}