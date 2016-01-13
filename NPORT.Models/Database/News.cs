using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NPORT.Models.Database
{
    public class News
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [ScaffoldColumn( false )]
        public string AuthorId { get; set; }

        [ScaffoldColumn( false )]
        public string Date { get; set; }
    }
}
