using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NPORT.Models.ViewModels.News;
using NPORT.Database.JSONDatabase;
using PagedList;
using NPORT.Models.Identity;
using Transliteration;

namespace NPORT.Controllers
{
    public class NewsController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? page, string sortOrder = "none")
        {
            IndexViewModel model = new IndexViewModel();

            var newsDb = new NewsDb();

            var newsList = newsDb.GetList();

            switch(sortOrder)
            {
                case "Title":

                    newsList.Sort((u1, u2) =>
                        {
                            if (string.Compare(u1.Title, u2.Title) >= 0)
                            {
                                return 1;
                            }
                            return -1;
                        }
                    );

                    break;

                case "Date":

                    newsList.Sort((u1, u2) =>
                        {
                            if (u1.Date.CompareTo(u2.Date) >= 0)
                            {
                                return -1;
                            }
                            return 1;
                        }
                    );

                    break;

                default:
                    break;
            }

            TempData["OrderBy"] = sortOrder;

            return View(newsList.ToPagedList( page ?? 1, 3 ));
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

                return RedirectToRoute("Home");
            }
            return View(newsModel);
        }

        [HttpGet]
        public ActionResult Detailed( string newsTitle, string newsId )
        {
            DetailedViewModel model = new DetailedViewModel();

            var newsDb = new NewsDb();

            model.News = newsDb.Find(newsId);

            var commentsDb = new CommentsDb();

            model.CommentList = commentsDb.GetListForNews(newsId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Detailed(Models.Database.Comment comment,string newsTitle, string newsId)
        {
            comment.NewsId = newsId;
            comment.AuthorId = User.Identity.GetUserId();

            AddComment( comment );

            return RedirectToRoute("News Details", new { newsTitle = newsTitle, newsId = newsId } );
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
        [Authorize(Roles = "Admin, Editor, Correspondent")]
        public ActionResult Remove(string newsId)
        {
            var newsDb = new NewsDb();
            var userDb = new Database.XMLDatabase.UsersDb();

            var user = userDb.Find(User.Identity.GetUserId());

            if (User.IsInRole("Correspondent") && user.Id != newsDb.Find(newsId).AuthorId)
                return View();

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