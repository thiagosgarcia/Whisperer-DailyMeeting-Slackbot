using System.Collections.Generic;
using System.Text.RegularExpressions;
using Whisperer.DependencyResolution;
using Whisperer.Models;

namespace Whisperer.Service.Commands
{
    public class MatchCommand
    {
        public List<ICommand> Commands => new List<ICommand>
        {
            Ioc.Container.GetInstance<PingCommand>(),
            Ioc.Container.GetInstance<ConfigCommand>(),
            Ioc.Container.GetInstance<HelpCommand>(),
        };
        public ICommand TryMatch(string s)
        {
            foreach (var comm in Commands)
            {
                foreach (var regex in comm.GetRegexes())
                {
                    var match = Regex.Match(s, regex);
                    if (match.Success)
                        return comm;
                }
            }

            return Ioc.Container.GetInstance<EmptyCommand>();
        }
    }
}