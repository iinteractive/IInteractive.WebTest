using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebTest
{
    public sealed class BrowserConfigElement : ConfigurationElement
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

        [ConfigurationProperty("name", IsRequired = true, IsKey=true)]
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

        [ConfigurationProperty("maximumAutomaticRedirections", DefaultValue="2")]
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

        [ConfigurationProperty("allowAutoRedirect", DefaultValue="true")]
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

        [ConfigurationProperty("userAgent", DefaultValue="Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.79 Safari/537.4")]
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

        [ConfigurationProperty("accept", DefaultValue="*/*")]
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

        [ConfigurationProperty("acceptCharset", DefaultValue="ISO-8859-1,utf-8;q=0.7,*;q=0.3")]
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

        [ConfigurationProperty("acceptLanguage", DefaultValue="en-US,en;q=0.8")]
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

    public class BrowserCollection : ConfigurationElementCollection
    {
        public BrowserCollection()
        {
            // Add one url to the collection.  This is 
            // not necessary; could leave the collection  
            // empty until items are added to it outside 
            // the constructor.
            BrowserConfigElement url =
                (BrowserConfigElement)CreateNewElement();
            Add(url);
        }

        public override
            ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return

                    ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override
            ConfigurationElement CreateNewElement()
        {
            return new BrowserConfigElement();
        }


        protected override
            ConfigurationElement CreateNewElement(
            string elementName)
        {
            return new BrowserConfigElement(elementName);
        }


        protected override Object
            GetElementKey(ConfigurationElement element)
        {
            return ((BrowserConfigElement)element).Name;
        }


        public new string AddElementName
        {
            get
            { return base.AddElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string ClearElementName
        {
            get
            { return base.ClearElementName; }

            set
            { base.ClearElementName = value; }

        }

        public new string RemoveElementName
        {
            get
            { return base.RemoveElementName; }
        }

        public new int Count
        {
            get { return base.Count; }
        }


        public BrowserConfigElement this[int index]
        {
            get
            {
                return (BrowserConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public BrowserConfigElement this[string Name]
        {
            get
            {
                return (BrowserConfigElement)BaseGet(Name);
            }
        }

        public int IndexOf(BrowserConfigElement url)
        {
            return BaseIndexOf(url);
        }

        public void Add(BrowserConfigElement url)
        {
            BaseAdd(url);
            // Add custom code here.
        }

        protected override void
            BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(BrowserConfigElement url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }
    }

    public class LinkCheckerConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("recursionLimit",
            IsDefaultCollection = false, DefaultValue=Int32.MaxValue)]
        [IntegerValidator(MinValue=0, MaxValue=Int32.MaxValue)]
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

        [ConfigurationProperty("requestTimeout",
            IsDefaultCollection = false, DefaultValue = 60)]
        [IntegerValidator(MinValue = 1, MaxValue = Int32.MaxValue)]
        public Int32 RequestTimeout
        {
            get
            {
                return (Int32)this["requestTimeout"];
            }
            set
            {
                this["requestTimeout"] = value;
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

        [ConfigurationProperty("browsers",
            IsDefaultCollection = false)]
        public BrowserCollection Browsers
        {
            get
            {
                BrowserCollection urlsCollection =
                (BrowserCollection)base["browsers"];
                return urlsCollection;
            }
        }
    }
}
