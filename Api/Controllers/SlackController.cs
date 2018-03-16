using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/reportbot")]
    public class ReportBot : Controller
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public JsonResult Get()
        {
            _logger.Debug("Get");
            return new JsonResult("Hello there from Get.");
        }

        [HttpPost]
        public JsonResult Post()
        {
            _logger.Debug("Handle");
            return new JsonResult("Hello there from Post.");
        }
    }
}