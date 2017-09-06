using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpecialtySelector.Startup))]

namespace SpecialtySelector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}