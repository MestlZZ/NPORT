using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NPORT.Startup))]
namespace NPORT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
