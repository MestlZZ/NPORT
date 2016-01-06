using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_22222.Startup))]
namespace _22222
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
