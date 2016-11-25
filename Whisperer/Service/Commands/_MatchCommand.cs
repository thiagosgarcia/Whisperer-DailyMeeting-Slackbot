using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using WebGrease.Css.Extensions;
using Whisperer.App_Start;
using Whisperer.DependencyResolution;
using Whisperer.Models;

namespace Whisperer.Service.Commands
{
    public class MatchCommand
    {
        private IEnumerable<Type> Commands;
        private List<Type> _excludedCommands = new List<Type>
        {
            typeof(EmptyCommand),
            typeof(AbstractCommand)
        };

        public MatchCommand()
        {
            Commands = Assembly.GetExecutingAssembly().GetTypes()
                .Where(FilterCommands);
        }

        private bool FilterCommands(Type i)
        {
            return typeof(AbstractCommand).IsAssignableFrom(i) &&
                _excludedCommands.All(x => x.Name != i.Name);
        }
        public ICommand TryMatch(string s)
        {
            foreach (var comm in Commands)
            {
                var instance = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance(comm) as AbstractCommand;
                foreach (var regex in instance.GetRegexes())
                {
                    var match = Regex.Match(s, regex);
                    if (match.Success)
                        return instance;
                }
            }

            return StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<EmptyCommand>();
        }
    }
}