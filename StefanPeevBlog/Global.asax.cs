using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using StefanPeevBlog.Models;
using StefanPeevBlog.Common;

namespace StefanPeevBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.Web.Mvc.ModelBinders.Binders[typeof(float)] = new SingleModelBinder();
            System.Web.Mvc.ModelBinders.Binders[typeof(double)] = new DoubleModelBinder();
            System.Web.Mvc.ModelBinders.Binders[typeof(decimal)] = new DecimalModelBinder();
            System.Web.Mvc.ModelBinders.Binders[typeof(float?)] = new SingleModelBinder();
            System.Web.Mvc.ModelBinders.Binders[typeof(double?)] = new DoubleModelBinder();
            System.Web.Mvc.ModelBinders.Binders[typeof(decimal?)] = new DecimalModelBinder();
        }
    }
}
