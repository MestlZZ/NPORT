using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using NPORT.Models.ViewModels.Shared;
using NPORT.Models.Database;

namespace NPORT.Models.ViewModels.News
{
    public class AddNewsViewModel : _TitleViewModel
    {
        public bool Access_AddNews { get; set; }

        public List<Role> Roles { get; set; }

        [HiddenInput( DisplayValue = false )]
        public string Id { get; set; }

        [Required]
        public string NewsTitle { get; set; }

        [Required]
        public string ShortInfo { get; set; }

        [Required]
        public int VisibleRange { get; set; }

        [Required]
        public string Content { get; set; }

        [HiddenInput( DisplayValue = false )]
        public string AuthorId { get; set; }

        [HiddenInput( DisplayValue = false )]
        public string Date { get; set; }
    }
}
