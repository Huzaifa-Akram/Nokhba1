using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Nokhba
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "JobSearchRoute",  // Route Name
                "JobSearch/{name}/{experience}/{location}", // URL Pattern
                "~/JobSearch.aspx", // Target Page
                false,
                new RouteValueDictionary { { "name", "" }, { "experience", "0" }, { "location", "" } }
            );
        }

    }
}