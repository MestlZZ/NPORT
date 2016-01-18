using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPORT.Models.ViewModels.Shared;

namespace NPORT.Models.ViewModels.News
{
    public class DetailedViewModel : _TitleViewModel
    {
        public Database.News CurrentNews{ get; set; }

        public int CurrentUserRoleId { get; set; }

        public string CurrentNewsAuthorName { get; set; }

        public bool CurrentUserAccess_EditNews { get; set; }

        public bool CurrentUserAccess_RemoveNews { get; set; }

        public string CurrentUserId { get; set; }

        public List<Database.Comment> CommentsList { get; set; }

        public List<string> UserListForComments { get; set; }
    }
}
