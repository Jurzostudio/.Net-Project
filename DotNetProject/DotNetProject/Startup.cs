using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotNetProject.Startup))]
namespace DotNetProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
