using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NPORT.Models.ViewModels.News;
using NPORT.Database.JSONDatabase;
using NPORT.Models.Identity;

namespace NPORT.Controllers
{
    public class NewsController : Controller
    {
        [HttpGet]
        public ActionResult Index(string sortBy = "none")
        {
            IndexViewModel model = new IndexViewModel();

            var newsDb = new NewsDb();

            model.NewsList = newsDb.GetList();

            if (sortBy == "Title")
                model.NewsList.Sort((u1, u2) => 
                    {
                        if (string.Compare(u1.Title, u2.Title) >= 0)
                        {
                            return 1;
                        }
                        return -1;
                    } 
                );

            if (sortBy == "Date")
                model.NewsList.Sort( ( u1, u2 ) =>
                {
                    if (u1.Date.CompareTo(u2.Date) >= 0)
                    {
                        return 1;
                    }
                    return -1;
                }
                );

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

                var newsDb = new NewsDb();

                newsDb.Add(news);

                return RedirectToRoute("News Details", new { newsId = news.Id });
            }
            return View(newsModel);
        }

        [HttpGet]
        public ActionResult Detailed( string newsId )
        {
            DetailedViewModel model = new DetailedViewModel();

            var newsDb = new NewsDb();

            model.News = newsDb.Find(newsId);

            var commentsDb = new CommentsDb();

            model.CommentList = commentsDb.GetListForNews(newsId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Detailed(Models.Database.Comment comment, string newsId)
        {
            comment.NewsId = newsId;
            comment.AuthorId = User.Identity.GetUserId();

            AddComment( comment );
            
            return RedirectToRoute("News Details", new { newsId = newsId } );
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(string newsId)
        {
            var model = new EditViewModel();

            var newsDb = new NewsDb();

            var news = newsDb.Find(newsId);

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
        [Authorize]
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

                var newsDb = new NewsDb();

                newsDb.Edit(news);

                return RedirectToRoute("Home");
            }

            return View(newsModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Remove(string newsId)
        {
            var newsDb = new NewsDb();

            newsDb.Remove(newsId);

            return View();
        }

        [Authorize]
        public void AddComment(Models.Database.Comment comment)
        {
            var commentsDb = new CommentsDb();

            commentsDb.Add(comment);
        }

        [Authorize]
        public ActionResult RemoveComment(int id, string url)
        {
            var userDb = new Database.XMLDatabase.UsersDb();

            var user = userDb.Find(User.Identity.GetUserId());

            var commentsDb = new CommentsDb();

            Models.Database.Comment comment = commentsDb.Find(id);

            if (user.Id == comment.AuthorId || user.GetRoleName() == "Admin")
            {
                commentsDb.Remove(id);
                return Redirect(url);                    
            }

            return RedirectToRoute("Home");           
        }
    }
}