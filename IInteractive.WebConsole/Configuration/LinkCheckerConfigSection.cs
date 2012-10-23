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

        [ConfigurationProperty("forbidden", IsRequired = false)]
        public ForbiddenCollection Forbidden
        {
            get
            {
                ForbiddenCollection forbiddenCollection =
                (ForbiddenCollection)base["forbidden"];
                return forbiddenCollection;
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
                    urlsCollection = new BrowserCollection();
                
                if(urlsCollection != null)
                    urlsCollection.Parent = this;

                return urlsCollection;
            }
        }

        [ConfigurationProperty("networkCredentials",
            IsDefaultCollection = false, IsRequired = false)]
        public NetworkCredentialsCollection NetworkCredentials
        {
            get
            {
                NetworkCredentialsCollection urlsCollection =
                (NetworkCredentialsCollection)base["networkCredentials"];
                return urlsCollection;
            }
        }

        [ConfigurationProperty("linksToIgnore",
            IsDefaultCollection = false, IsRequired = false)]
        public LinkToIgnoreCollection LinksToIgnore
        {
            get
            {
                LinkToIgnoreCollection urlsCollection =
                (LinkToIgnoreCollection)base["linksToIgnore"];
                return urlsCollection;
            }
        }

        protected override void PostDeserialize()
        {
            base.PostDeserialize();
            for (int i = 0; i < this.Seeds.Count; i++)
            {
                try
                {
                    Uri uri = new Uri(Seeds[i].Uri);
                    var seedHost = uri.Host;
                    for (int j = 0; j < Forbidden.Count; j++)
                    {
                        if (seedHost.Equals(Forbidden[j].Host, StringComparison.CurrentCultureIgnoreCase))
                        {
                            throw new ArgumentException(string.Format("A seed's host, {0}, matched to a forbidden host, {1}.", seedHost, Forbidden[j].Host));
                        }
                    }
                }
                catch(UriFormatException)
                {
                }
            }
        }
    }
}
