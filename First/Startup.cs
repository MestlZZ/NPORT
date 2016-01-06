using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(First.Startup))]
namespace First
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
