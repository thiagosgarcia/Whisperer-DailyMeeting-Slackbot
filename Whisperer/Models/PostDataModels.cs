using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whisperer.Models
{
    public class IncomingPostData
    {
        public string token { get; set; }
        public string team_id { get; set; }
        public string team_domain { get; set; }
        public string channel_id { get; set; }
        public string channel_name { get; set; }
        public long timestamp { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string text { get; set; }
        public string trigger_word { get; set; }
    }

    public class OutgoingPostData
    {
        public string text { get; set; }
        public Attachment[] attachments { get; set; }
    }
    public class CustomOutgoingPostData : OutgoingPostData
    {
        public string username { get; set; }
        public string icon_emoji { get; set; }
    }

    public class Attachment
    {
        public string fallback { get; set; }
        public string pretext { get; set; }
        public string color { get; set; }
        public AttachamentField[] fields { get; set; }
    }

    public class AttachamentField
    {
        public string title { get; set; }
        public string value { get; set; }
        public bool _short { get; set; }
    }

    public enum MessageError
    {
        invalid_payload,
        user_not_found,
        channel_not_found,
        channel_is_archived,
        action_prohibited,
        posting_to_general_channel_denied,
        too_many_attachments
    }
}