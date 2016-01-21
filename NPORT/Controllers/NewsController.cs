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
        public ActionResult AddNews( Models.Database.News news )
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
        public ActionResult Detailed( string newsId )
        {
            ViewBag.Id = newsId;
            return View();
        }

        [HttpPost]
        public ActionResult Detailed( Models.Database.Comment comment, string newsId )
        {
            comment.NewsId = newsId;
            comment.AuthorId = User.Identity.GetUserId();

            AddComment( comment );

            ViewBag.Id = newsId;
            return View();
        }

        [HttpGet]
        public ActionResult Edit( string newsId )
        {
            var model = NPORT.Database.JSONDatabase.NewsJson.Find(newsId);
            return View( model );
        }

        [HttpPost]
        [ValidateInput( false )]
        public ActionResult Edit( Models.Database.News news )
        {
            if (ModelState.IsValid)
            {
                NPORT.Database.JSONDatabase.NewsJson.Edit( news );
                return RedirectToAction( "Index" );
            }
            return View( news );
        }

        [HttpGet]
        public ActionResult Remove( string newsId )
        {
            var user = Database.XMLDatabase.Users.Find( User.Identity.GetUserId() );
            ApplicationRole role = null;
            if (user != null)
                role = Database.XMLDatabase.RoleDb.Find( user.RoleId );

            if (user != null)
            {
                Database.JSONDatabase.NewsJson.Remove( newsId );
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public void AddComment( Models.Database.Comment comment )
        {
            if (Request.IsAuthenticated)
            {
                Database.JSONDatabase.CommentsJson.Add(comment);
            }
        }

        public ActionResult RemoveComment( int id, string url )
        {
            if (Request.IsAuthenticated)
            {
                var user = Database.XMLDatabase.Users.Find(User.Identity.GetUserId());
                Models.Database.Comment comment = Database.JSONDatabase.CommentsJson.Find(id);
                if (user.Id == comment.AuthorId)
                {
                    Database.JSONDatabase.CommentsJson.Remove(id);
                    return Redirect(url + "#btn");                    
                }
            }
            return RedirectToAction("Index");           
        }
    }
}