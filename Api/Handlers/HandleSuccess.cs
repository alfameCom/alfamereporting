using Models.Slack;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Api.Handlers
{
    /// <summary>
    ///
    /// </summary>
    public class HandleSuccess : BaseHandler
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();
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
           label: 'Colleagues1',
           type: 'select',
           name: 'usernames1',
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
		   ]
         },{
           label: 'Colleagues2',
           type: 'select',
           name: 'usernames2',
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
		   ]
         },{
           label: 'Colleagues3',
           type: 'select',
           name: 'usernames3',
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
		   ]
         },{
           label: 'Colleagues4',
           type: 'select',
           name: 'usernames4',
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
		   ]
         },
         {
           label: 'Message',
           type: 'textarea',
           name: 'messsage'
         },
       ],
}
";

        /// <summary>
        ///
        /// </summary>
        /// <param name="payload"></param>
        public HandleSuccess(Payload payload)
        {
            _payload = payload;
            _accessToken = ConfigurationManager.AppSettings["SLACK_ACCESS_TOKEN"];
            _verificationToken = ConfigurationManager.AppSettings["SLACK_VERIFICATION_TOKEN"];
            if (String.IsNullOrEmpty(_accessToken) || String.IsNullOrEmpty(_verificationToken))
            {
                _logger.Trace("Hope this helps.");
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
                _logger.Trace("This sure must help");
                throw new Exception("This sure must help");
            }

            var message = new JObject();
            message.Add("trigger_id", _payload.trigger_id);
            message.Add("dialog", JObject.Parse(_dialog));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = client.PostAsync("https://slack.com/api/dialog.open", new StringContent(message.ToString(), Encoding.UTF8, "application/json")).Result;
            _logger.Trace($"Dialog message status code: {response.StatusCode}");

            var responseContent = response.Content.ReadAsStringAsync().Result;
            responseContent = HttpUtility.UrlDecode(responseContent);
            _logger.Trace($"Dialog message response: {responseContent}");
        }
    }
}