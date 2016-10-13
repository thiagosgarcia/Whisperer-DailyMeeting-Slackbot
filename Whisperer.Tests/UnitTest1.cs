using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whisperer.DependencyResolution;
using Whisperer.Models;
using Whisperer.Service;
using Xunit;
using Xunit.Should;

namespace Whisperer.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestPing()
        {
            Ioc.Initialize();
            var service = new PostBackService();
            var a = service.Command(new IncomingPostData
            {
                token = "ehMJtr7Bd1FCjq3hCFmwYij3",
                text = "ping"
            });
            a.ShouldBeSameAs(new CustomOutgoingPostData
            {
                text = "Pong!",
                icon_emoji = ":table_tennis_paddle_and_ball:",
                username = "Whisperer Bot"
            });
        }
    }
}
