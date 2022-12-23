using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article_Entities.ViewModels
{
    public class LoginModel
    {
        [StringLength(50), Required(ErrorMessage = "Please Enter a Valid Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Enter a Valid Password"), StringLength(20)]
        public string Password { get; set; }
    }


}
