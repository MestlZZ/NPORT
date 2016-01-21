using System.ComponentModel.DataAnnotations;

namespace NPORT.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display( Name = "Phone number" )]
        public string Login { get; set; }

        [Required]
        [DataType( DataType.Password )]
        [Display( Name = "Password" )]
        public string Password { get; set; }
    }
}