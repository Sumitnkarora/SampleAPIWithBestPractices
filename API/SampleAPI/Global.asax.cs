using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Configuration;

namespace SampleAPI
{
    /// <summary>
    /// Web Api Application class
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Triggered on application start
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SetUpGlobalVariables();
        }

        /// <summary>
        /// Sets up the global variables by reading data from the web.config file
        /// </summary>
        protected void SetUpGlobalVariables()
        {
            Global.GlobalVariables.APIKey = Guid.Parse(ConfigurationManager.AppSettings["APIKey"]);
            Global.GlobalVariables.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            Global.GlobalVariables.MaximumRecords = Convert.ToInt32(ConfigurationManager.AppSettings["MaximumRecords"]);
        }
    }
}
