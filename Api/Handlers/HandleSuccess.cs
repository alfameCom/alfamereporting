using Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Api.Handlers
{
    /// <summary>
    ///
    /// </summary>
    public class HandleSuccess
    {
        private readonly Payload _payload;
        private readonly string _accessToken;
        private readonly string _verificationToken;

        private readonly string _dialog = @"
{
    title: 'Praise your colleagues!',
       callback_id: 'submit-success',
       submit_label: 'Submit',
       elements: [
         {
           label: 'Colleagues',
           type: 'select',
           name: 'usernames',
		   options: [
			   {
				label: 'Antti the great',
				value: 'antti'
			   },
			   {
				label: 'Matias the elder',
				value: 'matias'
			   },
			   {
			    label: 'David the pääarkkitehti',
				value: 'david'
			   },
			   {
			    label: 'Petteri the n00b',
				value: 'petteri'
			   }
		   ],
           data_source: 'users'
         },
         {
           label: 'Message',
           type: 'textarea',
           name: 'messsage'
         },
       ],
}
";

        public HandleSuccess(Payload payload)
        {
            _payload = payload;
            _accessToken = ConfigurationManager.AppSettings["SLACK_ACCESS_TOKEN"];
            _verificationToken = ConfigurationManager.AppSettings["SLACK_VERIFICATION_TOKEN"];
            if (String.IsNullOrEmpty(_accessToken) || String.IsNullOrEmpty(_verificationToken))
            {
                Log.Trace("Hope this helps.");
                throw new Exception("Hope this helps.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Handle()
        {
            // check it is valid
            if (_payload.token != _verificationToken)
            {
                Log.Trace("This sure must help");
                throw new Exception("This sure must help");
            }

            var message = new JObject();
            message.Add("trigger_id", _payload.trigger_id);
            message.Add("dialog", JObject.Parse(_dialog));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = client.PostAsync("https://slack.com/api/dialog.open", new StringContent(message.ToString(), Encoding.UTF8, "application/json")).Result;
            Log.Trace($"Dialog message status code: {response.StatusCode}");

            var responseContent = response.Content.ReadAsStringAsync().Result;
            responseContent = HttpUtility.UrlDecode(responseContent);
            Log.Trace($"Dialog message response: {responseContent}");
        }
    }
}