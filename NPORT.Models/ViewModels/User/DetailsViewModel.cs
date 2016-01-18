using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPORT.Models.ViewModels.Shared;
using NPORT.Models.Database;
namespace NPORT.Models.ViewModels.User
{
    public class DetailsViewModel : _TitleViewModel
    {
        public List<Role> Roles { get; set; }
        public int CurrentUserRole { get; set; }
        public ApplicationUser UserInBase { get; set; }
        public string UserInBaseRole { get; set; }
    }
}
