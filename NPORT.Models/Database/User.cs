using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPORT.Models.Database
{
    public class User
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Nickname { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public bool Gender { get; set; }

        public string RegisterTime { get; set; }

        public int UserRoleId { get; set; }
    }
}
