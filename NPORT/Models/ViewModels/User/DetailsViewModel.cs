using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPORT.Models.Identity;

namespace NPORT.Models.ViewModels.User
{
    public class DetailsViewModel
    {
        public ApplicationUser UserInfo { get; set; }

        public List<ApplicationRole> RoleList { get; set; }
    }
}