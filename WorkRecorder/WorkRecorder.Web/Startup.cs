using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkRecorder.Web.Startup))]
namespace WorkRecorder.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
