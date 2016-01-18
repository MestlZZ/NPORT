using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPORT.Models.ViewModels.Shared;

namespace NPORT.Models.ViewModels.Home
{
    public class IndexViewModel : _TitleViewModel
    {
        public List<Database.News> NewsList { get; set; }

        public int CurrentUserRoleId { get; set; }

        public string newsClass { get; set; }
    }
}
