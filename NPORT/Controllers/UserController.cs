using System.Web.Mvc;
using NPORT.Identity;
using NPORT.Database.JSONDatabase;
using NPORT.Models.ViewModels.User;
using NPORT.Database.XMLDatabase;

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
            var userDb = new Database.XMLDatabase.UsersDb();

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
        public ActionResult Details(string Id, string Role)
        {
            var userDb = new UsersDb();

            var user = userDb.Find(Id);

            user.RoleId = Role ;

            CustomUserStore store = new CustomUserStore();

            store.Update(user);

            return RedirectToAction("UserList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Remove(string Id)
        {
            CustomUserStore store = new CustomUserStore();

            var user = store.FindById(Id);

            store.Delete( user );

            var newsDb = new NewsDb();

            var news = newsDb.GetList();

            foreach(var item in news)
            {
                if (item.AuthorId == Id)
                    newsDb.Remove( item.Id );
            }

            return View();
        }
    }
}
