using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BillManager.Startup))]
namespace BillManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
