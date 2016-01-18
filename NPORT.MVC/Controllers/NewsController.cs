using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NPORT.Models.ViewModels.News;
using NPORT.Database.XMLDatabase;
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
            var model = new AddNewsViewModel();

            var currentUser = Users.Find( User.Identity.GetUserId() );

            if (currentUser != null)
            {
                model.Access_AddNews = Roles.Find( currentUser.Role ).Access_AddNews;
            }
            else
                model.Access_AddNews = false;

            model.Roles = Roles.GetList();

            return View( model );
        }

        [HttpPost]
        [ValidateInput( false )]
        public ActionResult AddNews( Models.ViewModels.News.AddNewsViewModel newsModel )
        {
            if (ModelState.IsValid)
            {
                var news = new Models.Database.News();

                news.AuthorId = newsModel.AuthorId;
                news.Content = newsModel.Content;
                news.Date = newsModel.Date;
                news.Id = newsModel.Id;
                news.ShortInfo = newsModel.ShortInfo;
                news.Title = newsModel.Title;
                news.VisibleRange = newsModel.VisibleRange;

                news.AuthorId = User.Identity.GetUserId();
                NPORT.Database.JSONDatabase.NewsJson.Add( news );
                return RedirectToAction( "Index" );
            }
            return View( newsModel );
        }

        [HttpGet]
        public ActionResult Detailed( string NewsId )
        {
            var model = new DetailedViewModel();

            var user = Users.Find( User.Identity.GetUserId() );

            if (user != null)
            {
                var userRole = Roles.Find( user.Role );
                model.CurrentUserAccess_EditNews = userRole.Access_EditNews;
                model.CurrentUserAccess_RemoveNews = userRole.Access_RemoveNews;
                model.CurrentUserId = user.Id;
                model.CurrentUserRoleId = user.Role;
            }
            else
            {
                model.CurrentUserAccess_EditNews = false;
                model.CurrentUserAccess_RemoveNews = false;
                model.CurrentUserId = "guest";
                model.CurrentUserRoleId = 5;
            }

            model.CurrentNews = Database.JSONDatabase.NewsJson.Find( NewsId );
            model.CurrentNewsAuthorName = Users.Find( model.CurrentNews.AuthorId ).UserName;
            model.CommentsList = Database.JSONDatabase.CommentsJson.GetListForNewsId( NewsId );
            model.UserListForComments = Database.JSONDatabase.CommentsJson.GetListAuthorNameForComments( model.CommentsList );

            return View( model );
        }

        [HttpPost]
        public ActionResult Detailed( Models.Database.Comment comment, string NewsId )
        {
            comment.NewsId = NewsId;
            comment.AuthorId = User.Identity.GetUserId();

            AddComment( comment );

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
        public ActionResult Remove( string NewsId )
        {
            var user = Database.XMLDatabase.Users.Find( User.Identity.GetUserId() );
            Models.Database.Role role = null;
            if (user != null)
                role = Database.XMLDatabase.Roles.Find( user.Role );

            if (user != null && role.Access_RemoveNews && Request.IsAuthenticated)
            {
                Database.JSONDatabase.NewsJson.Remove( NewsId );
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

        public ActionResult RemoveComment( int Id, string url )
        {
            if (Request.IsAuthenticated)
            {
                var user = Database.XMLDatabase.Users.Find(User.Identity.GetUserId());
                Models.Database.Comment comment = Database.JSONDatabase.CommentsJson.Find(Id);
                if (user.Role <= 1 || user.Id == comment.AuthorId)
                {
                    Database.JSONDatabase.CommentsJson.Remove(Id);
                    return Redirect(url + "#btn");                    
                }
            }
            return RedirectToAction("Index");           
        }
    }
}