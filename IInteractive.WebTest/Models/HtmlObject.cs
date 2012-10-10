using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IInteractive.WebTest
{
    public abstract class HtmlObject : ITestableHttpItem, IEquatable<HtmlObject>
    {
        public string Path { get; set; }
        public string Html { get; set; }
        public Uri Root { get; set; }
        public Uri AbsoluteUri { get { return new Uri(Root, Path); } }
        public Uri ResultUrl { get; private set; }

        public bool Validate(out IEnumerable<HttpValidationError> errors)
        {
            errors = new List<HttpValidationError>();

            var results = new Browser().Get(AbsoluteUri);

            ResultUrl = results.ResultUrl;
            
            if(results.Error != null)
            {
                ((List<HttpValidationError>)errors).Add(results.Error);
            }

            return !errors.Any();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            
            if (obj is HtmlObject) return Equals((HtmlObject) obj);
            return false;
        }

        public bool Equals(HtmlObject obj)
        {
            return AbsoluteUri != null && AbsoluteUri.Equals(obj.AbsoluteUri);
        }

        public override int GetHashCode()
        {
            return (AbsoluteUri != null) ? AbsoluteUri.GetHashCode() : base.GetHashCode();
        } 
    }
}
