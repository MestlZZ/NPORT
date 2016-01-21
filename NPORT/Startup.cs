using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using NPORT.Models.Identity;
using NPORT.Identity;

[assembly: OwinStartupAttribute(typeof(NPORT.Startup))]
namespace NPORT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext( CustomUserManager.Create );
            app.CreatePerOwinContext<ApplicationSignInManager>( ApplicationSignInManager.Create );

            app.UseCookieAuthentication( new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString( "/Account/Login" ),
                Provider = new CookieAuthenticationProvider
                { 
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<CustomUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes( 30 ),
                        regenerateIdentity: ( manager, user ) => user.GenerateUserIdentityAsync( manager ) )
                }
            } );
            app.UseExternalSignInCookie( DefaultAuthenticationTypes.ExternalCookie );
            
            app.UseTwoFactorSignInCookie( DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes( 5 ) );

            app.UseTwoFactorRememberBrowserCookie( DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie );
        }
    }
}
