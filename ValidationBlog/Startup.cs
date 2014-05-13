using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ValidationBlog.Startup))]
namespace ValidationBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
