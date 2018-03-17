using System.IO;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace Api
{
    /// <summary>
    /// WebAPI application
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Startup
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}