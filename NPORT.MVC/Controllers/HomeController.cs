using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NPORT.Database.XMLDatabase;
using NPORT.Models.Database;

namespace NPORT.MVC
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }
    }
}
