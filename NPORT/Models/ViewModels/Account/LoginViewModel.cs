using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NPORT.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please fill 'Phone number' field")]
        [Display(Name = "Phone number")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please fill 'Password' field")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}