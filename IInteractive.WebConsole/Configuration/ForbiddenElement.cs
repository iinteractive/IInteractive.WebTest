using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class ForbiddenElement : ConfigurationElement
    {
        public ForbiddenElement(String Host)
        {
            this.Host = Host;
        }

        public ForbiddenElement()
        {
        }

        [ConfigurationProperty("host", IsRequired = true, IsKey = true)]
        public String Host
        {
            get
            {
                return (String)this["host"];
            }
            set
            {
                this["host"] = value;
            }
        }
    }
}
