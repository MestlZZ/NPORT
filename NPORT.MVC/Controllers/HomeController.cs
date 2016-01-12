using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "correspondent")]
        public ActionResult AddNews()
        {
            return View();
        }
        [Authorize( Roles = "admin" )]
        [Authorize( Roles = "correspondent" )]
        [HttpPost]
        public ActionResult AddNews(NPORT.Models.Database.News news)
        {
            if (ModelState.IsValid)
            {
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