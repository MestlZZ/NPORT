using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using NPORT.Models;

namespace NPORT
{
    // Настройка диспетчера входа для приложения.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager( CustomUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task SignInAsync( ApplicationUser user, bool isPersistent, bool rememberBrowser )
        {
            return base.SignInAsync( user, isPersistent, rememberBrowser );
        }

        public override async Task<SignInStatus> PasswordSignInAsync( string userName, string password, bool isPersistent, bool shouldLockout )
        {
            ApplicationUser user = this.UserManager.FindByName(userName);
            
            var result = await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);

            return (result);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<CustomUserManager>(), context.Authentication);
        }
    }
}
