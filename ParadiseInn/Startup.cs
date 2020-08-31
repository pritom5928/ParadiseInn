using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParadiseInn.Startup))]
namespace ParadiseInn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
