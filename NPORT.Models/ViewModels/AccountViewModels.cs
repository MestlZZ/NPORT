using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NPORT.Models.ViewModels
{

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name (Email)")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

    }
    // public class RegisterViewModel
    //{
    //    [Required]
    //    public string Login { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    //    public string ConfirmPassword { get; set; }

    //    [Required]
    //    public int Age { get; set; }
    //}
}