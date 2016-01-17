using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace NPORT.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddNews()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput( false )]
        public ActionResult AddNews( NPORT.Models.Database.News news )
        {
            if (ModelState.IsValid)
            {
                news.AuthorId = User.Identity.GetUserId();
                NPORT.Database.JSONDatabase.NewsJson.Add( news );
                return RedirectToAction( "Index" );
            }
            return View( news );
        }

        [HttpGet]
        public ActionResult Detailed( string NewsId )
        {
            ViewBag.Id = NewsId;
            return View();
        }
        [HttpGet]
        public ActionResult Edit( string NewsId )
        {
            var model = NPORT.Database.JSONDatabase.NewsJson.Find(NewsId);
            return View( model );
        }

        [HttpPost]
        [ValidateInput( false )]
        public ActionResult Edit( NPORT.Models.Database.News news )
        {
            if (ModelState.IsValid)
            {
                NPORT.Database.JSONDatabase.NewsJson.Edit( news );
                return RedirectToAction( "Index" );
            }
            return View( news );
        }

        [HttpGet]
        public ActionResult Remove( string NewsId )
        {
            NPORT.Database.JSONDatabase.NewsJson.Remove( NewsId );
            return View();
            //return RedirectToAction("Index");
        }
    }
}