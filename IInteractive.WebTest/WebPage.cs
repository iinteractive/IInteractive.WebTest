using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IInteractive.WebTest
{
    public class WebPage : HtmlObject, ITestableHttpItem, IEquatable<HtmlObject>, IComparable<WebPage>
    {
        public WebPage(Uri url, Browser browser)
        {
            Path = url;
            RequestUrl = url;
            Browser = browser;
        }

        public Uri RequestUrl { get; private set; }
        public Uri Path { get; set; }
        public Browser Browser { get; set; }
        public List<HyperLink> Links { get; set; }
        public List<Image> Images { get; set; }
        public List<JavaScript> JavaScripts { get; set; }
        public List<StyleSheet> StyleSheets { get; set; }

        public HttpValidationError Error { get; set; }

        public void Get()
        {
            var results = Browser.GetHtml(Path);

            Path = results.ResultUrl;

            if (results.Error == null)
            {
                Parse(results.Html);
            }
            else
            {
                Error = results.Error;
            }
        }

        private void Parse(string html)
        {
            Html = html;
            Links = HyperLink.Generate(Path, Html);
            Images = Image.Generate(Path, Html);
            JavaScripts = JavaScript.Generate(Path, Html);
            StyleSheets = StyleSheet.Generate(Path, Html);
        }

        public bool Validate(out IEnumerable<HttpValidationError> errors)
        {
            var tempErrors = new List<HttpValidationError>();

            var validatedItems =
                Links.Union<HtmlObject>(Images).Union<HtmlObject>(JavaScripts).Union<HtmlObject>(StyleSheets).Distinct().ToList();

            NumberOfItems = validatedItems.Count;

            foreach (var item in validatedItems)
            {
                IEnumerable<HttpValidationError> itemErrors;
                item.Validate(out itemErrors);
                tempErrors.AddRange(itemErrors);
            }

            errors = tempErrors;

            return !errors.Any();
        }

        public int NumberOfItems { get; set; }

        public override bool Equals(object obj)
        {
            return (obj as WebPage).RequestUrl.Equals(this.RequestUrl);
        }

        public int CompareTo(WebPage obj)
        {
            return obj.RequestUrl.ToString().CompareTo(this.RequestUrl.ToString());
        }
    }
}
