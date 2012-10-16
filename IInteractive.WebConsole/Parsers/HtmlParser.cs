using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IInteractive.WebConsole
{
    public class HtmlParser
    {
        public static readonly Regex CommentRegex = new Regex("<!--.*?-->", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static readonly Regex ImageRegex = new Regex("<img[^>]+?src=[\"|'](.*?)[\"|'](.*?)>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static readonly Regex JavaScriptRegex = new Regex("<script[^>]+?src=[\"|'](.*?)[\"|'].*?>(.*?)</script>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static readonly Regex HyperLinkRegex = new Regex("<a[^>]+?href=[\"|'](.*?)[\"|'].*?>(.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static readonly Regex StyleSheetRegex = new Regex("<link[^>]+?href=[\"|'](.*?)[\"|'].*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public HtmlParser(HttpRequestResult HttpRequestResult)
        {
            this.HttpRequestResult = HttpRequestResult;
        }

        public HttpRequestResult HttpRequestResult;
        public string ContentWithCommentsRemoved
        {
            get
            {
                return CommentRegex.Replace(HttpRequestResult.Content, "");
            }
        }

        public List<Link> Parse()
        {
            List<Link> links = new List<Link>();

            links.AddRange(GenerateImages());
            links.AddRange(GenerateJavaScripts());
            links.AddRange(GenerateHyperLinks());
            links.AddRange(GenerateStyleSheets());

            return links.Distinct().ToList();
        }

        public List<Image> GenerateImages()
        {
            var matches = ImageRegex.Matches(this.ContentWithCommentsRemoved);

            var images = new List<Image>();

            foreach (Match match in matches)
            {
                Image image = new Image(HttpRequestResult.ResultUrl, match.Groups[1].Value);
                image.Source = match.Groups[1].Value;
                image.Content = match.Groups[0].Value;
                images.Add(image);
            }

            return images.Distinct().ToList();
        }

        public List<JavaScript> GenerateJavaScripts()
        {
            var matches = JavaScriptRegex.Matches(this.ContentWithCommentsRemoved);

            var scripts = new List<JavaScript>();

            foreach (Match match in matches)
            {
                JavaScript javaScript = new JavaScript(HttpRequestResult.ResultUrl, match.Groups[1].Value);
                javaScript.Source = match.Groups[1].Value;
                javaScript.Content = match.Groups[0].Value;
                scripts.Add(javaScript);
            }

            return scripts.Distinct().ToList();
        }

        

        public List<HyperLink> GenerateHyperLinks()
        {
            var matches = HyperLinkRegex.Matches(this.ContentWithCommentsRemoved);

            var links = new List<HyperLink>();

            foreach (Match match in matches)
            {
                if (!match.Groups[1].Value.StartsWith("javascript:", StringComparison.CurrentCultureIgnoreCase) && !match.Groups[1].Value.StartsWith("mailto:", StringComparison.CurrentCultureIgnoreCase) && !match.Groups[1].Value.StartsWith("tel:", StringComparison.CurrentCultureIgnoreCase))
                {
                    HyperLink hyperLink = new HyperLink(HttpRequestResult.ResultUrl, match.Groups[1].Value);
                    hyperLink.Text = match.Groups[2].Value;
                    hyperLink.Content = match.Groups[0].Value;
                    links.Add(hyperLink);
                }
            }

            return links.Distinct().ToList();
        }

        public List<StyleSheet> GenerateStyleSheets()
        {
            var matches = StyleSheetRegex.Matches(this.ContentWithCommentsRemoved);

            var sheets = new List<StyleSheet>();

            foreach (Match match in matches)
            {
                if (match.Groups[0].Value.IndexOf("rel=\"stylesheet\"", StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    StyleSheet styleSheet = new StyleSheet(HttpRequestResult.ResultUrl, match.Groups[1].Value);
                    styleSheet.Content = match.Groups[0].Value;
                    sheets.Add(styleSheet);
                }
            }

            return sheets.Distinct().ToList();
        }
    }
}
