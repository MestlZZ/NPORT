using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NPORT.Database.JSONDatabase;
using NPORT.Models.Database;

namespace NPORT.MVC
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            News news = new News();
            news.AuthorId = "none";
            news.Content = "My best news!!\n Returned ivano fiuleas";
            news.Date = "16.12.2005";
            news.Id = "guid";
            news.Title = "Fantasy";
            NewsJson.AddNews( news );
            return View();
        }
    }
}
