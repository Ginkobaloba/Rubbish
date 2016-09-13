using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rubbish.Startup))]
namespace Rubbish
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
