using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace NPORT.Models.Database
{
    public class ApplicationUser
    {
        public ApplicationUser()
        { }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Phone { get; set; }

        public int Role { get; set; }
    }
}
