using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetSit.Startup))]
namespace PetSit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
