using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.Ajax.Utilities;
using Whisperer.DependencyResolution;
using Whisperer.Models;
using ConfigurationModel = Whisperer.Models.Configuration;

namespace Whisperer.Service.Commands
{
    public class ConfigCommand : AbstractCommand
    {
        private IService<ConfigurationModel> _service;

        public ConfigCommand()
        {
            _service = Ioc.Container.GetInstance<IService<ConfigurationModel>>();
        }
        public override string[] GetRegexes()
        {
            return new string[] { "^whisy config" };
        }

        private string GetLanguage(string s)
        {
            var regex =  "(?<=(language:))[ ]*\\w+(?= |$)";
            return Regex.Match(s, regex).Value;
        }
        public override async Task<CustomOutgoingPostData> Action(IncomingPostData data)
        {
            var config = _service.GetAll().First();
            var language = GetLanguage(data.text);
            if (!language.IsNullOrWhiteSpace())
            {
                config.Language = language;
            }
            _service.Update(config);

            return new CustomOutgoingPostData()
            {
                text = "All done here =)"
            };
        }
    }
}