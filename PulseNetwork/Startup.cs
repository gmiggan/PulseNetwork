using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PulseNetwork.Startup))]
namespace PulseNetwork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
