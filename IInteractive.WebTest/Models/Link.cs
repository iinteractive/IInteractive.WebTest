using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IInteractive.WebTest
{
    /// <summary>
    /// Intended to be base class for Html based parsers.
    /// </summary>
    public abstract class Link : IEquatable<Link>, IEquatable<HttpRequestResult>
    {
        public Link(Uri Root, string Path)
        {
            this.Root = Root;
            this.Path = Path;

            try
            {
                new Uri(Root, Path);
            }
            catch(UriFormatException ex)
            {
                this.Error = ex;
            }
        }

        private Link()
        {
        }

        public string Path { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// The root property holds the url of the place this link was retrieved.
        /// </summary>
        public Uri Root { get; set; }
        /// <summary>
        /// The absolute uri of the original link.  Returns null if the Uri 
        /// constructor fails to put togethor a Uri.
        /// </summary>
        public Uri AbsoluteUri
        {
            get
            {
                try
                {
                    return new Uri(Root, Path);
                }
                catch (UriFormatException ex)
                {
                    this.Error = ex;
                    return null;
                }
            }
        }
        public UriFormatException Error { get; private set; }
        public bool WasRetrieved = false;

        public bool Equals(Link obj)
        {
            return AbsoluteUri != null && AbsoluteUri.Equals(obj.AbsoluteUri);
        }
        /// <summary>
        /// Will be used to link the set of HttpRequestResults and the Links 
        /// for each HttpRequestResult to generate information on which pages 
        /// contain links that are broken.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Equals(HttpRequestResult obj)
        {
            return AbsoluteUri != null && AbsoluteUri.Equals(obj.RequestUrl);
        }

        public override int GetHashCode()
        {
            return (AbsoluteUri != null) ? AbsoluteUri.GetHashCode() : base.GetHashCode();
        } 
    }
}
