using System.Web.Http;
using System.Web.Http.Cors;

namespace Api
{
    /// <summary>
    /// Configure API
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("http://localhost:8080,https://alfamereporting.azurewebsites.net", "*", "*");
            cors.SupportsCredentials = true;
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}