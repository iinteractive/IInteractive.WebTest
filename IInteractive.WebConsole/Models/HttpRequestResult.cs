using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IInteractive.WebConsole;

namespace IInteractive.WebConsole
{
    public class HttpRequestResult : IEquatable<HttpRequestResult>, IEquatable<Link>
    {
        private Regex HtmlRegex = new Regex("html", RegexOptions.IgnoreCase);
        private Regex CssRegex = new Regex("css", RegexOptions.IgnoreCase);

        public HttpValidationError Error { get; set; }
        public string ContentType { get; set; }
        public Uri RequestUrl { get; set; }
        public Uri ResultUrl { get; set; }
        public string Content { get; set; } // Will be null if !IsHtml && !IsCss
        public List<Link> Links { get; private set; } // Will be null if !IsHtml && !IsCss
        public void Parse()
        {
            if (IsCss)
            {
                CssParser parser = new CssParser(this);
                Links = parser.Parse();
            }
            else if (IsHtml)
            {
                HtmlParser parser = new HtmlParser(this);
                Links = parser.Parse();
            }
        }
        public bool IsHtml
        {
            get
            {
                if (this.ContentType == null) 
                    return false;

                var matches = HtmlRegex.Matches(this.ContentType);

                return matches.Count != 0;
            }
        }
        public bool IsCss
        {
            get
            {
                if (this.ContentType == null)
                    return false;

                var matches = CssRegex.Matches(this.ContentType);

                return matches.Count != 0;
            }
        }

        public bool Equals(HttpRequestResult obj)
        {
            return this.ResultUrl.Equals(obj.ResultUrl);
        }

        public bool Equals(Link obj)
        {
            return this.RequestUrl.Equals(obj.AbsoluteUri);
        }
    }
}
