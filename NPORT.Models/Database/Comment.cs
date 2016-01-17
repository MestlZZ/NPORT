using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPORT.Models.Database
{
    public class Comment
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string Text { get; set; }

        public string Date { get; set; }

        public string NewsId { get; set; }
    }
}
