using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webgame.Startup))]
namespace webgame
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
