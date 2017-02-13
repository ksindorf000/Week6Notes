using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Day1AttrRoutes.Startup))]
namespace Day1AttrRoutes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
