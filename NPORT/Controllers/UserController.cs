using System;
using System.Web.Mvc;
using NPORT.Models;

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
            return View( Database.XMLDatabase.UsersDb.GetList() );
        }        

        public ActionResult Details( string Id )
        {
            return View( Database.XMLDatabase.UsersDb.Find(Id) );
        }

        [HttpPost]
        public ActionResult Details( string Id, string Role )
        {
            var user = Database.XMLDatabase.UsersDb.Find(Id);
            user.RoleId =  Role ;
            CustomUserStore store = new CustomUserStore();

            store.Update( user );

            return RedirectToAction("UserList");
        }

        public ActionResult Remove( string Id )
        {
            CustomUserStore store = new CustomUserStore();

            var user = store.FindById(Id);

            store.Delete( user );

            var news = Database.JSONDatabase.NewsDb.GetList();

            foreach(var item in news)
            {
                if (item.AuthorId == Id)
                    Database.JSONDatabase.NewsDb.Remove( item.Id );
            }

            return View();
        }
    }
}
