using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IInteractive.WebTest
{
    public class Image : HtmlObject
    {
        public static readonly Regex RegularExpression = new Regex("<img.+?src=[\"|'](.+?)[\"|'].*?>(.*?)/>", RegexOptions.IgnoreCase);
        public static readonly Regex Style2 = new Regex("<img.+?src=[\"|'](.+?)[\"|'].*?>(.*?)>", RegexOptions.IgnoreCase);

        public static List<Image> Generate(Uri root, string html)
        {
            var matches = RegularExpression.Matches(html);

            var images = new List<Image>();

            foreach (Match match in matches)
            {
                images.Add(
                    new Image()
                    {
                        Root = root,
                        Source = match.Groups[1].Value,
                        Html = match.Groups[0].Value,
                        Path = match.Groups[1].Value
                    });
            }

            matches = Style2.Matches(html);

            foreach (Match match in matches)
            {
                images.Add(
                    new Image()
                    {
                        Root = root,
                        Source = match.Groups[1].Value,
                        Html = match.Groups[0].Value,
                        Path = match.Groups[1].Value
                    });
            }

            return images;
        }

        public string Source { get; set; }
    }
}
