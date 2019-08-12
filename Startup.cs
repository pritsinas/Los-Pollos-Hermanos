using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LosPollosHermanos.Startup))]
namespace LosPollosHermanos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
