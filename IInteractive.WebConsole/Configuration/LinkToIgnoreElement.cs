using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class LinkToIgnoreElement : ConfigurationElement, IEquatable<Link>
    {
        public LinkToIgnoreElement()
        {
        }

        [ConfigurationProperty("path", IsRequired = true, IsKey = true)]
        public String Path
        {
            get
            {
                return (String)this["path"];
            }
            set
            {
                this["path"] = value;
            }
        }

        [ConfigurationProperty("isUri", IsRequired = false, IsKey = false, DefaultValue = "true")]
        public bool IsUri
        {
            get
            {
                return (bool)this["isUri"];
            }
            set
            {
                this["isUri"] = value;
            }
        }

        public bool Equals(Link arg)
        {
            if (this.IsUri && arg.AbsoluteUri != null)
            {
                return new Uri(this.Path).Equals(arg.AbsoluteUri);
            }
            else
            {
                return arg.Path.Equals(this.Path);
            }
        }

        protected override void PostDeserialize()
        {
            base.PostDeserialize();
            if (this.IsUri)
            {
                try
                {
                    new Uri(Path);
                }
                catch(UriFormatException ex)
                {
                    throw new ArgumentException("The IsUri attribute must be set to false when the path is not a valid URI.", "IsUri", ex);
                }
            }
        }
    }
}
