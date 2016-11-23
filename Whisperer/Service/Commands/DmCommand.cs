using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;
using Whisperer.DependencyResolution;
using Whisperer.Models;
using ConfigurationModel = Whisperer.Models.Configuration;

namespace Whisperer.Service.Commands
{
    public class DmCommand : AbstractCommand
    {
        private readonly IAnswerService _answerService;

        public DmCommand(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        public override string[] GetRegexes()
        {
            return new string[]
            {
                "^whisy dm",
                "^whisy daily",
                "^whisy daily meeting"
            };
        }

        private bool hasToStart(string s)
        {
            var regex = "[ ]start(?= |$)";
            return Regex.IsMatch(s, regex);
        }

        public override async Task<CustomOutgoingPostData> Action(IncomingPostData data)
        {
            if (hasToStart(data.text))
                return await UserAnswers();
            return new CustomOutgoingPostData()
            {
                text = "Sorry, didn't understand what you've just said",
                icon_emoji = ":thinking_face:",
            };
        }

        private async Task<CustomOutgoingPostData> UserAnswers()
        {
            var answers = (await _answerService.GetByMeeting()).ToList();

            var users = new List<User>();
            answers.ForEach(x =>
            {
                if (users.All(u => u.UserId != x.User.UserId))
                    users.Add(x.User);
            });

            return new CustomOutgoingPostData
            {
                attachments = users.Select(u =>
                new Attachment
                {
                    pretext = u.Username,
                    fields = answers.Where(a => a.User.UserId == u.UserId)
                        .Select(a => new AttachamentField
                        {
                            title = a.Question.Text,
                            value = a.Text
                        }).ToArray()
                }).ToArray()
            };
        }
    }
}