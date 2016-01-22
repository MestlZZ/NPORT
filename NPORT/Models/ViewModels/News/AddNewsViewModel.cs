using System.ComponentModel.DataAnnotations;

namespace NPORT.Models.ViewModels.News
{
    public class AddNewsViewModel
    {
        [Required(ErrorMessage ="Please fill 'Title' field")]
        [StringLength(99, ErrorMessage = "Title must have less than 100 and more than {2} symbols", MinimumLength =2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please fill 'ShortInfo' field")]
        [StringLength(400,ErrorMessage = "ShortInfo must have less than 400 symbols")]
        public string ShortInfo { get; set; }

        public bool Visible { get; set; }

        [Required(ErrorMessage = "Please fill 'Content' field")]
        [StringLength(30000,ErrorMessage = "Content must have less than 10000 symbols")]
        public string Content { get; set; }
    }
}