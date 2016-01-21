using System.Web.Mvc;
using NPORT.Identity;
using NPORT.Database.JSONDatabase;
using NPORT.Models.ViewModels.User;
using NPORT.Database.XMLDatabase;
using Microsoft.AspNet.Identity;

namespace NPORT.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserList()
        {
            var userDb = new UsersDb();

            return View(userDb.GetList());
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var userDb = new UsersDb();
            var roleDb = new RoleDb();

            var model = new DetailsViewModel();

            model.UserInfo = userDb.Find(id);
            model.RoleList = roleDb.GetList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Details(string id, string Role)
        {
            var userDb = new UsersDb();

            var user = userDb.Find(id);

            user.RoleId = Role ;

            CustomUserStore store = new CustomUserStore();

            store.Update(user);

            return RedirectToAction("UserList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Remove(string id)
        {
            if (User.Identity.GetUserId() != id)
            {
                CustomUserStore store = new CustomUserStore();

                var user = store.FindById(id);

                store.Delete( user );

                var newsDb = new NewsDb();

                var news = newsDb.GetList();

                foreach (var item in news)
                {
                    if (item.AuthorId == id)
                        newsDb.Remove( item.Id );
                }

                return View();
            }

            return RedirectToRoute("User list");
        }
    }
}
