using System.Web.Mvc;

namespace Api
{
    /// <summary>
    /// Filter configuration
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Register filters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler.AiHandleErrorAttribute());
        }
    }
}