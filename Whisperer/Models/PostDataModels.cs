using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

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
    public class ApiResponse
    {
        public bool ok { get; set; }
        public string error { get; set; }
        public string warning { get; set; }
    }

    #region userslist
    //https://slack.com/api/users.list
    //token
    //presence (optional)

    public class OutgoingUserParameters
    {
        public string token { get; set; }
        public byte presence { get; set; }

        public string toJson()
        {
            return Json.Encode(this);
        }

        public IEnumerable<KeyValuePair<string, string>> ToPairs()
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("token", this.token),
                new KeyValuePair<string, string>("presence", this.presence.ToString())
            };
        }
    }
    public class UsersList : ApiResponse
    {
        public ApiUser[] members { get; set; }

    }

    public class ApiUser
    {
        public string id { get; set; }
        public string team_id { get; set; }
        public string name { get; set; }
        public bool deleted { get; set; }
        public string status { get; set; }
        public string color { get; set; }
        public string real_name { get; set; }
        public string tz { get; set; }
        public string tz_label { get; set; }
        public string tz_offset { get; set; }
        public ApiProfile profile { get; set; }
        public bool is_admin { get; set; }
        public bool is_owner { get; set; }
        public bool has_2fa { get; set; }
        public bool has_files { get; set; }
        public string presence { get; set; }
    }

    public class ApiProfile
    {
        public string avatar_hash { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string real_name { get; set; }
        public string email { get; set; }
        public string skype { get; set; }
        public string phone { get; set; }
        public string image_24 { get; set; }
        public string image_32 { get; set; }
        public string image_48 { get; set; }
        public string image_72 { get; set; }
        public string image_192 { get; set; }
    }
    #endregion
    #region channelsList
    //https://slack.com/api/channels.info
    //token
    //channel

    public class ChannelsList : ApiResponse
    {
        public ApiChannel channels { get; set; }
    }
    public class ApiChannel
    {
        public string id { get; set; }
        public string name { get; set; }
        public long created { get; set; }
        public string creator { get; set; }
        public bool is_archived { get; set; }
        public bool is_general { get; set; }
        public bool is_member { get; set; }
        public bool is_starred { get; set; }
        public ApiUser[] members { get; set; }
        public ChannelParameter topic { get; set; }
        public ChannelParameter purpose { get; set; }
        public string last_read { get; set; }
        //public ChannelParameter latest { get; set; }
        public int unread_count { get; set; }
        public int unread_count_display { get; set; }

    }

    public class ChannelParameter
    {
        public string value { get; set; }
        public string creator { get; set; }
        public long last_set { get; set; }
    }

    #endregion
    #region getPresence

    //https://slack.com/api/users.getPresence
    //token
    //user
    public class Presence : ApiResponse
    {
        public string presence { get; set; }
    }

    public class AuthedUserPresence : Presence
    {
        public bool online { get; set; }
        public bool auto_away { get; set; }
        public bool manual_away { get; set; }
        public int connection_count { get; set; }
        public long last_activity { get; set; }
    }
    #endregion
}