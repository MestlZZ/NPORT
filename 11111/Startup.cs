using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_11111.Startup))]
namespace _11111
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
