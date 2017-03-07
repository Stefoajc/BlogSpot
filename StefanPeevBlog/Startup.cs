using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StefanPeevBlog.Startup))]
namespace StefanPeevBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
