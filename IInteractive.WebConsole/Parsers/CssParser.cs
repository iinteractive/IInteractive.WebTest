using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IInteractive.WebConsole
{
    public class CssParser
    {
        public static readonly Regex CommentRegex = new Regex("/\\*.*?\\*/", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static readonly Regex CssUrlRegex = new Regex("url\\s*?\\(\\s*?('|\")(.*?)\\1\\s*?\\)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public CssParser(HttpRequestResult HttpRequestResult)
        {
            this.HttpRequestResult = HttpRequestResult;
        }

        public HttpRequestResult HttpRequestResult;

        public List<Link> Parse()
        {
            List<Link> links = new List<Link>();
            links.AddRange(GenerateCssUrls());
            return links;
        }

        public List<CssUrl> GenerateCssUrls()
        {
            var matches = CssUrlRegex.Matches(this.ContentWithCommentsRemoved);

            var cssUrls = new List<CssUrl>();

            foreach (Match match in matches)
            {
                CssUrl cssUrl = new CssUrl(HttpRequestResult.ResultUrl, match.Groups[2].Value);
                cssUrl.Content = match.Groups[0].Value;
                cssUrls.Add(cssUrl);
            }

            return cssUrls.Distinct().ToList();
        }

        public string ContentWithCommentsRemoved
        {
            get
            {
                return CommentRegex.Replace(HttpRequestResult.Content, "");
            }
        }

        public List<Link> GenerateUrls()
        {
            throw new NotImplementedException();
        }
    }
}
