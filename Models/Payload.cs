using System.Collections.Generic;

namespace Models
{
    public class Action
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Team
    {
        public string id { get; set; }
        public string domain { get; set; }
    }

    public class Channel
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Payload
    {
        public string type { get; set; }
        public List<Action> actions { get; set; }
        public string callback_id { get; set; }
        public Team team { get; set; }
        public Channel channel { get; set; }
        public User user { get; set; }
        public string action_ts { get; set; }
        public string message_ts { get; set; }
        public string attachment_id { get; set; }
        public string token { get; set; }
        public bool is_app_unfurl { get; set; }
        public string response_url { get; set; }
        public string trigger_id { get; set; }

        public Payload()
        {
            actions = new List<Action>();
        }
    }
}