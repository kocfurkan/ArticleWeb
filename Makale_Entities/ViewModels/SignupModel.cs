using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities.ViewModels
{
    public class SignupModel
    {
        [StringLength(50), Required(ErrorMessage = "Please Enter a Valid Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Enter a Valid Email"), StringLength(50), EmailAddress(ErrorMessage = "Please Enter a Valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter a Valid Password"), StringLength(20, ErrorMessage = "Password Cannot be Longer Than 50 Characters")]
        public string Password { get; set; }

        [DisplayName("Confirm Password"), Required(ErrorMessage = "Please Confirm Password"), StringLength(20), Compare(nameof(Password), ErrorMessage = "Passwords Doesn't Match")]
        public string PasswordConfirm { get; set; }

    }
}
