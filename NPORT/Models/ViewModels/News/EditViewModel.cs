using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NPORT.Models.ViewModels.News
{
    public class EditViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortInfo { get; set; }

        public bool Visible { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public string Date { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
    }
}