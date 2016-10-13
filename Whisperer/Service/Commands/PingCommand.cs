using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Whisperer.Models;

namespace Whisperer.Service.Commands
{
    public class PingCommand : AbstractCommand
    {
        public PingCommand()
        {

        }
        public override string[] GetRegexes()
        {
            return new string[] { "^ping" };
        }

        public override async Task<CustomOutgoingPostData> Action(IncomingPostData data)
        {
            var pingData = GetParameters(data);
            Attachment att = null;
            if (!pingData.IsNullOrWhiteSpace())
                att = new Attachment()
                {
                    fields = new AttachamentField[]
                    {
                        new AttachamentField()
                        {
                            value = pingData
                        },
                    }
                };

            return new CustomOutgoingPostData
            {
                text = "Pong!",
                icon_emoji = ":table_tennis_paddle_and_ball:",
                username = "Whisperer Bot",
                attachments = new Attachment[] { att }
            };
        }
    }
}