using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IInteractive.WebTest
{
    public class HyperLink : HtmlObject
    {
        public static readonly Regex RegularExpression = new Regex("<a.+?href=[\"|'](.+?)[\"|'].*?>(.*?)</a>", RegexOptions.IgnoreCase);

        public static List<HyperLink> Generate(Uri root, string html)
        {
            var matches = RegularExpression.Matches(html);

            var links = new List<HyperLink>();

            foreach(Match match in matches)
            {
                if(!match.Groups[1].Value.StartsWith("javascript:")) {
                    links.Add(
                        new HyperLink()
                            {
                                Root = root,
                                Text = match.Groups[2].Value,
                                Href = match.Groups[1].Value,
                                Html = match.Groups[0].Value,
                                Path = match.Groups[1].Value
                            });
                }
            }

            return links;
        }

        public string Text { get; set; }
        public string Href { get; set; }
    }
}
