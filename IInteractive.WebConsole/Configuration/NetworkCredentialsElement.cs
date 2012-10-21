using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole.Configuration
{
    public class NetworkCredentialsElement : ConfigurationElement
    {
        public NetworkCredentialsElement()
        {
        }

        public NetworkCredentialsElement(String User, String Password)
        {
            this.User = User;
            this.Password = Password;
        }

        [ConfigurationProperty("uriPrefix", IsRequired = true, IsKey = true)]
        public String UriPrefix
        {
            get
            {
                return (String)this["uriPrefix"];
            }
            set
            {
                this["uriPrefix"] = value;
            }
        }

        [ConfigurationProperty("user", IsRequired = true, IsKey = false)]
        public String User
        {
            get
            {
                return (String)this["user"];
            }
            set
            {
                this["user"] = value;
            }
        }

        [ConfigurationProperty("password", IsRequired = true, IsKey = false)]
        public String Password
        {
            get
            {
                return (String)this["password"];
            }
            set
            {
                this["password"] = value;
            }
        }
    }
}
