using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IInteractive.WebTest
{
    /// <summary>
    /// Intended to be used for parsing link elements out of an HTML page.
    /// </summary>
    public class StyleSheet : HtmlObject
    {
        public static readonly Regex RegularExpression = new Regex("<link.+?href=[\"|'](.+?)[\"|'].*?>(.*?)</link>", RegexOptions.IgnoreCase);
        public static readonly Regex Style2 = new Regex("<link.+?href=[\"|'](.+?)[\"|'].*?>(.*?)/>", RegexOptions.IgnoreCase);

        public static List<StyleSheet> Generate(Uri root, string html)
        {
            var matches = RegularExpression.Matches(html);

            var sheets = new List<StyleSheet>();

            foreach (Match match in matches)
            {
                if (match.Groups[0].Value.IndexOf("rel=\"stylesheet\"", StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    sheets.Add(
                        new StyleSheet()
                            {
                                Root = root,
                                Href = match.Groups[1].Value,
                                Html = match.Groups[0].Value
                            });
                }
            }

            matches = Style2.Matches(html);

            foreach (Match match in matches)
            {
                if (match.Groups[0].Value.IndexOf("rel=\"stylesheet\"", StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    sheets.Add(
                        new StyleSheet()
                        {
                            Root = root,
                            Href = match.Groups[1].Value,
                            Html = match.Groups[0].Value,
                            Path = match.Groups[1].Value
                        });
                }
            }

            return sheets;
        }

        public string Href { get; set; }
    }
}
