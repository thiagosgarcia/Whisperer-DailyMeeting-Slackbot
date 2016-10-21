using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Whisperer.Models;

namespace Whisperer.Service.Commands
{
    public class EmptyCommand : AbstractCommand
    {
        public EmptyCommand()
        {

        }
        public override string[] GetRegexes()
        {
            return null;
        }

        public override async Task<CustomOutgoingPostData> Action(IncomingPostData data)
        {
            return null;
        }
    }
}