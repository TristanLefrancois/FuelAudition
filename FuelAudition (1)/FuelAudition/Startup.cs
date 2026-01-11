using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FuelAudition.Startup))]
namespace FuelAudition
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
