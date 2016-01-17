using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.UI;
using System.ComponentModel.DataAnnotations;
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
            return View( Database.XMLDatabase.Users.GetList() );
        }        

        public ActionResult Details( string Id )
        {
            return View( Database.XMLDatabase.Users.Find(Id) );
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
