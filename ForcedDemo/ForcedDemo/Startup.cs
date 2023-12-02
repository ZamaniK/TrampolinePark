using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForcedDemo.Startup))]
namespace ForcedDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
