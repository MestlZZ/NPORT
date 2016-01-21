using System.ComponentModel.DataAnnotations;

namespace NPORT.Models.ViewModels.News
{
    public class AddNewsViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortInfo { get; set; }

        public bool Visible { get; set; }

        [Required]
        public string Content { get; set; }
    }
}