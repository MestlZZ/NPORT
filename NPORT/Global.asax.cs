using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Principal;
using System;

namespace NPORT
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes( RouteTable.Routes );
        }

        protected void Application_AuthenticateRequest( object sender, EventArgs args )
        {
            if (Context.User != null)
            {
                var userDb = new Database.XMLDatabase.UsersDb();

                string role = userDb.FindByUsername(Context.User.Identity.Name).GetRoleName();

                string[] roles = new string[1];

                roles[0] = role;

                GenericPrincipal principal = new GenericPrincipal(Context.User.Identity, roles);

                Context.User = principal;
            }
        }
    }
}
