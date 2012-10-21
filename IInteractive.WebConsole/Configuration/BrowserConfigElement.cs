using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class BrowserConfigElement : ConfigurationElement
    {
        public BrowserConfigElement(String Name
            , Int16 MaximumAutomaticRedirections, Boolean AllowAutoRedirect
            , String UserAgent, String Accept, String AcceptCharset
            , String AcceptLanguage)
        {
            this.Name = Name;
            this.MaximumAutomaticRedirections = MaximumAutomaticRedirections;
            this.AllowAutoRedirect = AllowAutoRedirect;
            this.UserAgent = UserAgent;
            this.Accept = Accept;
            this.AcceptCharset = AcceptCharset;
            this.AcceptLanguage = AcceptLanguage;
        }

        public BrowserConfigElement(String Name)
        {
            this.Name = Name;
        }

        public BrowserConfigElement()
        {
        }

        [ConfigurationProperty("name", IsRequired = true, DefaultValue = "default", IsKey = true)]
        public String Name
        {
            get
            {
                return (String)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("maximumAutomaticRedirections", DefaultValue = "2")]
        public Int16 MaximumAutomaticRedirections
        {
            get
            {
                return (Int16)this["maximumAutomaticRedirections"];
            }
            set
            {
                this["maximumAutomaticRedirections"] = value;
            }
        }

        [ConfigurationProperty("allowAutoRedirect", DefaultValue = "true")]
        public Boolean AllowAutoRedirect
        {
            get
            {
                return (Boolean)this["allowAutoRedirect"];
            }
            set
            {
                this["allowAutoRedirect"] = value;
            }
        }

        [ConfigurationProperty("userAgent", DefaultValue = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.79 Safari/537.4")]
        public String UserAgent
        {
            get
            {
                return (String)this["userAgent"];
            }
            set
            {
                this["userAgent"] = value;
            }
        }

        [ConfigurationProperty("accept", DefaultValue = "*/*")]
        public String Accept
        {
            get
            {
                return (String)this["accept"];
            }
            set
            {
                this["accept"] = value;
            }
        }

        [ConfigurationProperty("acceptCharset", DefaultValue = "ISO-8859-1,utf-8;q=0.7,*;q=0.3")]
        public String AcceptCharset
        {
            get
            {
                return (String)this["acceptCharset"];
            }
            set
            {
                this["acceptCharset"] = value;
            }
        }

        [ConfigurationProperty("acceptLanguage", DefaultValue = "en-US,en;q=0.8")]
        public String AcceptLanguage
        {
            get
            {
                return (String)this["acceptLanguage"];
            }
            set
            {
                this["acceptLanguage"] = value;
            }
        }
    }
}
