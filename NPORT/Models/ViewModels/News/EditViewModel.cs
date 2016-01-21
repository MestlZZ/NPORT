using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NPORT.Models.ViewModels.News
{
    public class EditViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Maximum length of title: 50 symbols", MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Maximum length of short info: 250 symbols", MinimumLength = 5)]
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