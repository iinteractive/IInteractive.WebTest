using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class SeedCollection : ConfigurationElementCollection
    {
        public SeedCollection()
        {
            // Add one url to the collection.  This is 
            // not necessary; could leave the collection  
            // empty until items are added to it outside 
            // the constructor.
            SeedConfigElement url =
                (SeedConfigElement)CreateNewElement();
            Add(url);
        }

        public static explicit operator List<String>(SeedCollection collection)
        {
            var list = new List<String>();
            foreach (SeedConfigElement seed in collection)
            {
                list.Add(seed.Uri);
            }
            return list;
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
            return new SeedConfigElement();
        }


        protected override
            ConfigurationElement CreateNewElement(
            string elementName)
        {
            return new SeedConfigElement(elementName);
        }


        protected override Object
            GetElementKey(ConfigurationElement element)
        {
            return ((SeedConfigElement)element).Uri;
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


        public SeedConfigElement this[int index]
        {
            get
            {
                return (SeedConfigElement)BaseGet(index);
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

        new public SeedConfigElement this[string Name]
        {
            get
            {
                return (SeedConfigElement)BaseGet(Name);
            }
        }

        public int IndexOf(SeedConfigElement url)
        {
            return BaseIndexOf(url);
        }

        public void Add(SeedConfigElement url)
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

        public void Remove(SeedConfigElement url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.Uri);
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
