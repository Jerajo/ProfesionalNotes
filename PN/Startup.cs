using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PN.Startup))]
namespace PN
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
