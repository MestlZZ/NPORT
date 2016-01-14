using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NPORT.Models;

namespace NPORT.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private static CustomUserManager _customUserManager;

        public CustomUserManager UserManager
        {
            get
            {
                return _customUserManager ??
                       (_customUserManager = HttpContext.GetOwinContext().GetUserManager<CustomUserManager>());
            }
        }
        public async Task<bool> Authenticate( string name, string password )
        {
            if (await UserManager.FindAsync( name, password ) != null)
            {
                return true;
            }

            return false;
        }
        
}