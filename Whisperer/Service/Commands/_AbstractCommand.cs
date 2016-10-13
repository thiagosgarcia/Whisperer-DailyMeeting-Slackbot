
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Whisperer.Models;

public interface ICommand
{
    string[] GetRegexes();
    Task<CustomOutgoingPostData> Action(IncomingPostData data);
    string GetParameters(IncomingPostData data);
}

public abstract class AbstractCommand : ICommand
{
    public abstract string[] GetRegexes();
    public abstract Task<CustomOutgoingPostData> Action(IncomingPostData data);
    public string GetParameters(IncomingPostData data)
    {
        foreach (var regex in GetRegexes())
        {
            var match = Regex.Match(data.text, regex, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            if (match.Success)
                return Regex.Replace(data.text, regex, "").Trim();
        }
        return null;
    }
}