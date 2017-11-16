using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Paradise.Startup))]
namespace Paradise
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
