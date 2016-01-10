using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPORT.Models.Database
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Access_RemoveNews { get; set; }

        public bool Access_AddNews { get; set; }

        public bool Access_EditNews { get; set; }
    }
}
