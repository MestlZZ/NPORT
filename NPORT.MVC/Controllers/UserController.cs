using System;
using System.Web.Mvc;
using NPORT.Models;
using NPORT.Models.ViewModels.Shared;
using NPORT.Models.ViewModels.User;
using Microsoft.AspNet.Identity;
using NPORT.Database.XMLDatabase;
using System.Collections.Generic;

namespace NPORT.MVC.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            return View(model);
        }

        public ActionResult UserList()
        {
            return View( );
        }        

        public ActionResult Details( string Id )
        {
            DetailsViewModel model = new DetailsViewModel();
            ApplicationUser user = NPORT.Database.XMLDatabase.Users.Find(User.Identity.GetUserId());
            model.CurrentUserRole = user.Role;
            model.Roles = NPORT.Database.XMLDatabase.Roles.GetList();
            user = Database.XMLDatabase.Users.Find(Id);
            model.UserInBase = user.ConvertUser();
            model.UserInBaseRole = user.GetRole();
            return View( model);
        }

        [HttpPost]
        public ActionResult Details( string Id, string Role )
        {
            var user = Database.XMLDatabase.Users.Find(Id);
            user.Role = Convert.ToInt32( Role );
            CustomUserStore store = new CustomUserStore();

            store.Update( user );

            return RedirectToAction("UserList");
        }

        public ActionResult Remove( string Id )
        {
            CustomUserStore store = new CustomUserStore();

            var user = store.FindById(Id);

            store.Delete( user );

            var news = Database.JSONDatabase.NewsJson.GetList();

            foreach(var item in news)
            {
                if (item.AuthorId == Id)
                    Database.JSONDatabase.NewsJson.Remove( item.Id );
            }

            return View();
        }
    }
}
