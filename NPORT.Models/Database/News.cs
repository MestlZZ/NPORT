using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NPORT.Models.Database
{
    public class News
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortInfo { get; set; }

        [Required]
        public int VisibleRange { get; set; }

        [Required]
        public string Content { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Date { get; set; }
    }
}
