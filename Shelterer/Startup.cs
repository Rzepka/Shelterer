using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shelterer.Startup))]
namespace Shelterer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
