using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Avengers.Startup))]
namespace Avengers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
