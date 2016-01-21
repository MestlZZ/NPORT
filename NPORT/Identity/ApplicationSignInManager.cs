using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using NPORT.Models.Identity;

namespace NPORT.Identity
{
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
