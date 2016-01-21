using System.Web.Mvc;
using NPORT.Identity;
using NPORT.Database.JSONDatabase;

namespace NPORT.MVC.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserList()
        {
            var userDb = new Database.XMLDatabase.UsersDb();

            return View( userDb.GetList() );
        }        

        public ActionResult Details( string Id )
        {
            var userDb = new Database.XMLDatabase.UsersDb();

            return View( userDb.Find(Id) );
        }

        [HttpPost]
        public ActionResult Details( string Id, string Role )
        {
            var userDb = new Database.XMLDatabase.UsersDb();

            var user = userDb.Find(Id);

            user.RoleId = Role ;

            CustomUserStore store = new CustomUserStore();

            store.Update( user );

            return RedirectToAction("UserList");
        }

        public ActionResult Remove( string Id )
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
