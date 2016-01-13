using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace NPORT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize(Roles = "Admin, Correspondent")]
        public ActionResult AddNews()
        {
            return View();
        }
        [Authorize( Roles = "Admin, Correspondent")]
        [HttpPost]
        public ActionResult AddNews(NPORT.Models.Database.News news)
        {
            if (ModelState.IsValid)
            {
                news.AuthorId = User.Identity.GetUserId();
                NPORT.Database.JSONDatabase.NewsJson.Add( news );
                return RedirectToAction("Index");
            }
            return View( news );
        }

        [HttpGet]
        public ActionResult Detailed( string NewsId )
        {
            ViewBag.Id = NewsId;
            return View();
        }
    }
}