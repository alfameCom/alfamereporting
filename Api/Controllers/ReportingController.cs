using Api.Handlers;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
            Log.Trace($"Get()");
            var jsonObject = JObject.Parse(_testMessage);
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
            Log.Trace($"Post uri: {message.RequestUri}");

            var content = message.Content.ReadAsStringAsync().Result;
            Log.Trace($"Post content: {content}");
            content = HttpUtility.UrlDecode(content);
            Log.Trace($"Post content (decoded): {content}");

            // remove odd start
            content = content.StartsWith("payload=") ? content.Substring(8, content.Length - 8) : content;

            // try to parse payload
            Payload payload = null;
            try
            {
                // we hope there is a spoon
                payload = JsonConvert.DeserializeObject<Payload>(content);
                Log.Trace($"Post serialize ok: {payload != null}");
            }
            catch (Exception)
            {
                // there is no spoon
            }
            if (payload != null)
            {
                HandlePayload(payload);
            }

            return Request.CreateResponse(HttpStatusCode.OK, JObject.Parse(_testMessage));
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