using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class BrowserCollection : ConfigurationElementCollection
    {
        public BrowserCollection()
        {
            // Add one url to the collection.  This is 
            // not necessary; could leave the collection  
            // empty until items are added to it outside 
            // the constructor.
            BrowserConfigElement url =
                new BrowserConfigElement();
            Add(url);
        }

        public LinkCheckerConfigSection Parent { get; internal set; }

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
                var elem = (BrowserConfigElement)BaseGet(index);
                if (elem != null)
                    elem.Parent = this;
                return elem;
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
                var elem = (BrowserConfigElement)BaseGet(Name);
                if (elem != null)
                    elem.Parent = this;
                return elem;
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
}
