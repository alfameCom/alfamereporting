using Api.Handlers;
using Models.Slack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Api.Controllers
{
    /// <summary>
    /// TODO
    /// </summary>
    [Route("api/reportingbot")]
    public class ReportingController : BaseController
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        private string _command = @"
{
   'text': 'Welcome!',
   'attachments': [
       {
           'text': 'Please choose what to report',
           'fallback': 'Please contact the Pää-arkkitehti',
           'callback_id': 'wopr_command',
           'color': '#3AA3E3',
           'attachment_type': 'default',
           'actions': [
               {
                   'name': 'command',
                   'text': 'Success',
                   'type': 'button',
                   'value': 'success',
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

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(JObject))]
        public HttpResponseMessage Get()
        {
            _logger.Trace($"Get()");
            var jsonObject = JObject.Parse(_command);
            return Request.CreateResponse(HttpStatusCode.OK, jsonObject);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(JObject))]
        public HttpResponseMessage Post(HttpRequestMessage message)
        {
            _logger.Trace($"Post uri: {message.RequestUri}");

            var content = message.Content.ReadAsStringAsync().Result;
            _logger.Trace($"Post content: {content}");
            content = HttpUtility.UrlDecode(content);
            _logger.Trace($"Post content (decoded): {content}");

            // remove odd start
            content = content.StartsWith("payload=") ? content.Substring(8, content.Length - 8) : content;

            // try to parse payload
            Payload payload = null;
            try
            {
                // we hope there is a spoon
                payload = JsonConvert.DeserializeObject<Payload>(content);
                _logger.Trace($"Post serialize ok: {payload != null}");
            }
            catch (Exception)
            {
                // there is no spoon
            }
            if (payload != null)
            {
                HandlePayload(payload);
            }

            return Request.CreateResponse(HttpStatusCode.OK, JObject.Parse(_command));
        }

        private HttpResponseMessage HandlePayload(Payload payload)
        {
            var action = payload.actions.Single();
            switch (action.value)
            {
                case "success":
                    Task.Run(() =>
                    {
                        var handler = new HandleSuccess(payload);
                        handler.Handle();
                    });
                    break;

                default:
                    throw new Exception($"We have no spoon for {action.value}");
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}