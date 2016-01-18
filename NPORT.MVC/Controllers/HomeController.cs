using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NPORT.Models.ViewModels.Home;
namespace NPORT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new IndexViewModel();

            model.newsClass = "right";
            model.NewsList = Database.JSONDatabase.NewsJson.GetList();
            var user = Database.XMLDatabase.Users.Find( User.Identity.GetUserId() );
            if (user != null)
                model.CurrentUserRoleId = user.Role;
            else
                model.CurrentUserRoleId = 5;

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}