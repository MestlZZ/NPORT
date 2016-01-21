using System.Collections.Generic;
using PagedList;

namespace NPORT.Models.ViewModels.News
{
    public class IndexViewModel
    {
        public IPagedList<Database.News> NewsList { get; set; }
    }
}