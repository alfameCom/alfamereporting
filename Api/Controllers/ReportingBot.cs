using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLog;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/reportingbot")]
    public class ReportingBot : Controller
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        private string _testMessage = @"
{
   'text': 'Welcome!',
   'attachments': [
       {
           'text': 'Please choose what to report',
           'fallback': 'You are unable to report',
           'callback_id': 'wopr_command',
           'color': '#3AA3E3',
           'attachment_type': 'default',
           'actions': [
               {
                   'name': 'comand',
                   'text': 'Success',
                   'type': 'button',
                   'value': 'success'
               },
               {
                   'name': 'command',
                   'text': 'Äänikirjabonus',
                   'type': 'button',
                   'value': 'aanikirjabonus'
               }

           ]
       }
   ]
}
";

        [HttpGet]
        public JsonResult Get()
        {
            System.Diagnostics.Trace.WriteLine($"Get (SD) Trace");
            System.Diagnostics.Debug.WriteLine($"Get (SD) Debug");
            _logger.Trace($"Get (NLOG) Trace");
            _logger.Warn($"Get (NLOG) Warn");
            _logger.Debug($"Get (NLOG) Debug");
            _logger.Info($"Get (NLOG) Info");
            var message = JObject.Parse(_testMessage);
            return new JsonResult(message);
        }

        [HttpPost]
        public JsonResult Post()
        {
            _logger.Trace($"Post (NLOG)");
            var message = JObject.Parse(_testMessage);
            return new JsonResult(message);
        }

        //[HttpPost]
        //public JsonResult Post([FromBody] string content)
        //{
        //    _logger.Debug($"Post");
        //    var message = JObject.Parse(_testMessage);
        //    return new JsonResult(message);
        //}
    }
}