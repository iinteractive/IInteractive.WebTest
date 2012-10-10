using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IInteractive.WebTest
{
    /// <summary>
    /// Intended to be used for parsing script elements out of an HTML page.
    /// </summary>
    public class JavaScript : HtmlObject
    {
        public static readonly Regex RegularExpression = new Regex("<script.+?src=[\"|'](.+?)[\"|'].*?>(.*?)</script>", RegexOptions.IgnoreCase);

        public static List<JavaScript> Generate(Uri root, string html)
        {
            var matches = RegularExpression.Matches(html);

            var scripts = new List<JavaScript>();

            foreach (Match match in matches)
            {
                scripts.Add(
                    new JavaScript()
                    {
                        Root = root,
                        Source = match.Groups[1].Value,
                        Html = match.Groups[0].Value,
                        Path = match.Groups[1].Value
                    });
            }

            return scripts;
        }

        public string Source { get; set; }
    }
}
