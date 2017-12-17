using Paradise.Models;
using System.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Paradise
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var raisedException = Server.GetLastError();

            // Process exception
            YFUTEntities db = new YFUTEntities();
            Error error = new Error();

            if (Session["userName"]?.ToString() != null)
            {
                string userName = Session["userName"]?.ToString();
                error.admin_ID = db.Admins.Where(a => a.userName == userName).ToList()[0].admin_ID;
            }

            error.errorDate = DateTime.Now;
            error.errorDesc = raisedException.Message;

            db.Errors.Add(error);
            db.Entry(error).State = EntityState.Added;
            db.SaveChanges();

            //Disabled so that custom error pages will run
            //Server.ClearError();
        }
    }
}
