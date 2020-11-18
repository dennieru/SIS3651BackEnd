using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Host.Startup))]
namespace Host
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            InitConfig(app);
        }
    }
}
