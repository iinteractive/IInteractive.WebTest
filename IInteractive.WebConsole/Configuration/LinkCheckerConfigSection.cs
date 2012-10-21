using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class LinkCheckerConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("name",
            IsDefaultCollection = false, IsRequired = false,
            DefaultValue = "Web Site Link Checker Tests")]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("description",
            IsDefaultCollection = false, IsRequired = false,
            DefaultValue = "Tests links across a web site.")]
        public string Description
        {
            get
            {
                return (string)this["description"];
            }
            set
            {
                this["description"] = value;
            }
        }

        [ConfigurationProperty("testResultsFile",
            IsRequired = false,
            DefaultValue = "test-results.trx")]
        public string TestResultsFile
        {
            get
            {
                return (string)this["testResultsFile"];
            }
            set
            {
                this["testResultsFile"] = value;
            }
        }

        [ConfigurationProperty("recursionLimit",
            IsDefaultCollection = false, DefaultValue = Int32.MaxValue)]
        [IntegerValidator(MinValue = 0, MaxValue = Int32.MaxValue)]
        public Int32 RecursionLimit
        {
            get
            {
                return (Int32)this["recursionLimit"];
            }
            set
            {
                this["recursionLimit"] = value;
            }
        }

        [ConfigurationProperty("maxRemoteAutomaticRedirects",
            DefaultValue = -1)]
        [IntegerValidator(MinValue = -1, MaxValue = Int32.MaxValue)]
        public Int32 MaxRemoteAutomaticRedirects
        {
            get
            {
                return (Int32)this["maxRemoteAutomaticRedirects"];
            }
            set
            {
                this["maxRemoteAutomaticRedirects"] = value;
            }
        }

        [ConfigurationProperty("timeout",
            IsDefaultCollection = false, DefaultValue = 60)]
        [IntegerValidator(MinValue = 1, MaxValue = Int32.MaxValue)]
        public Int32 Timeout
        {
            get
            {
                return (Int32)this["timeout"];
            }
            set
            {
                this["timeout"] = value;
            }
        }

        [ConfigurationProperty("maxCrawlTime",
            IsDefaultCollection = false, DefaultValue = Int32.MaxValue)]
        [IntegerValidator(MinValue = 1, MaxValue = Int32.MaxValue)]
        public Int32 MaxCrawlTime
        {
            get
            {
                return (Int32)this["maxCrawlTime"];
            }
            set
            {
                this["maxCrawlTime"] = value;
            }
        }

        [ConfigurationProperty("seeds", IsRequired = true)]
        public SeedCollection Seeds
        {
            get
            {
                SeedCollection urlsCollection =
                (SeedCollection)base["seeds"];
                return urlsCollection;
            }
        }

        [ConfigurationProperty("browsers",
            IsDefaultCollection = false, IsRequired = false)]
        public BrowserCollection Browsers
        {
            get
            {
                BrowserCollection urlsCollection =
                (BrowserCollection)base["browsers"];
                if (urlsCollection == null)
                    return new BrowserCollection();
                else
                    return urlsCollection;
            }
        }

        [ConfigurationProperty("networkCredentials",
            IsDefaultCollection = false, IsRequired = false)]
        public NetworkCredentialsElement NetworkCredentials
        {
            get
            {
                NetworkCredentialsElement urlsCollection =
                (NetworkCredentialsElement)base["networkCredentials"];
                return urlsCollection;
            }
        }
    }
}
