using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class SeedConfigElement : ConfigurationElement
    {
        public SeedConfigElement(String Uri)
        {
            this.Uri = Uri;
        }

        public SeedConfigElement()
        {
        }

        [ConfigurationProperty("uri", IsRequired = true, IsKey = true)]
        public String Uri
        {
            get
            {
                return (String)this["uri"];
            }
            set
            {
                this["uri"] = value;
            }
        }
    }
}
