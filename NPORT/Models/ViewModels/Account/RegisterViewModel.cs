﻿using System.ComponentModel.DataAnnotations;

namespace NPORT.Models.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please fill 'Phone' field")]
        [Display(Name = "Phone number")]
        [RegularExpression(@"[+]{1}[0-9]{12}", ErrorMessage = "Please, enter phone number in this format: +123456789123 for example")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please fill 'Nickname' field")]
        [StringLength(20, ErrorMessage = "Nickname must have not less than {2} symbols and not more than 20", MinimumLength = 3)]
        [Display(Name = "Nickname")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Please fill 'Password' field")]
        [StringLength(16, ErrorMessage = "Password must have not less than {2} symbols and not more than 16 symbols", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not equal")]
        public string ConfirmPassword { get; set; }
    }
}
