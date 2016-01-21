using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NPORT.Models.ViewModels.News;
using NPORT.Database.JSONDatabase;
using Transliteration;

namespace NPORT.Controllers
{
    public class NewsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();

            model.NewsList = NewsDb.GetList();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Correspondent")]
        public ActionResult AddNews()
        {
            var model = new AddNewsViewModel();

            model.Visible = true;

            return View(model);
        }

        [HttpPost]
        [ValidateInput( false )]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Correspondent")]
        public ActionResult AddNews( AddNewsViewModel newsModel )
        {
            if (ModelState.IsValid)
            {
                var news = new Models.Database.News
                {
                    AuthorId = User.Identity.GetUserId(),
                    Content = newsModel.Content,
                    ShortInfo = newsModel.ShortInfo,
                    Title = newsModel.Title,
                    Visible = newsModel.Visible
                };

                NewsDb.Add(news);

                return RedirectToRoute("Home");
            }
            return View(newsModel);
        }

        [HttpGet]
        public ActionResult Detailed( string newsTitle , string newsId  )
        {     
            DetailedViewModel model = new DetailedViewModel();

            model.News = NewsDb.Find(newsId);

            model.CommentList = CommentsDb.GetListForNews(newsId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Detailed(Models.Database.Comment comment, string newsTitle, string newsId)
        {            
            comment.NewsId = newsId;
            comment.AuthorId = User.Identity.GetUserId();

            AddComment( comment );
            
            return RedirectToRoute("News Details", new { newsTitle = newsTitle, newsId = newsId } );
        }

        [HttpGet]
        public ActionResult Edit(string newsId)
        {
            var model = new EditViewModel();

            var news = NewsDb.Find(newsId);

            model.Id = news.Id;
            model.ShortInfo = news.ShortInfo;
            model.Title = news.Title;
            model.Visible = news.Visible;
            model.AuthorId = news.AuthorId;
            model.Content = news.Content;
            model.Date = news.Date;

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel newsModel)
        {
            if (ModelState.IsValid)
            {
                var news = new Models.Database.News();

                news.Id = newsModel.Id;
                news.ShortInfo = newsModel.ShortInfo;
                news.Title = newsModel.Title;
                news.Visible = newsModel.Visible;
                news.AuthorId = newsModel.AuthorId;
                news.Content = newsModel.Content;
                news.Date = newsModel.Date;

                NewsDb.Edit(news);

                return RedirectToRoute("Home");
            }

            return View(newsModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Remove(string newsId)
        {
            var user = Database.XMLDatabase.UsersDb.Find(User.Identity.GetUserId());

            ApplicationRole role = null;
            if (user != null)
                role = Database.XMLDatabase.RoleDb.Find(user.RoleId);

            if (user != null)
            {
                NewsDb.Remove(newsId);
                return View();
            }
            else
            {
                return RedirectToRoute("Home");
            }
        }

        [Authorize]
        public void AddComment(Models.Database.Comment comment)
        {
                CommentsDb.Add(comment);
        }

        [Authorize]
        public ActionResult RemoveComment(int id, string url)
        {
            var user = Database.XMLDatabase.UsersDb.Find(User.Identity.GetUserId());
            Models.Database.Comment comment = CommentsDb.Find(id);

            if (user.Id == comment.AuthorId || user.GetRoleName() == "Admin")
            {
                CommentsDb.Remove(id);
                return Redirect(url);                    
            }

            return RedirectToRoute("Home");           
        }
    }
}