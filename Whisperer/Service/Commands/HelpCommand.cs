using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Whisperer.Models;

namespace Whisperer.Service.Commands
{
    public class HelpCommand : AbstractCommand
    {
        public override string[] GetRegexes()
        {
            return new string[]
            {
                "^help",
                "^whisy ([?]|(help))"
            };
        }


        public override async Task<CustomOutgoingPostData> Action(IncomingPostData data)
        {
            return new CustomOutgoingPostData
            {
                text = "Sorry, still no help page here...",
                icon_emoji = ":thinking_face:",
                username = "Embarrassed Whisperer Bot"
            };
        }
    }
}