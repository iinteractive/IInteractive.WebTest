using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class LinkToIgnoreCollection : ConfigurationElementCollection
    {
        public LinkToIgnoreCollection()
        {
            
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
            return new LinkToIgnoreElement();
        }

        protected override
            ConfigurationElement CreateNewElement(
            string elementName)
        {
            return new LinkToIgnoreElement();
        }


        protected override Object
            GetElementKey(ConfigurationElement element)
        {
            return ((LinkToIgnoreElement)element).Path;
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


        public LinkToIgnoreElement this[int index]
        {
            get
            {
                return (LinkToIgnoreElement)BaseGet(index);
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

        new public LinkToIgnoreElement this[string Name]
        {
            get
            {
                return (LinkToIgnoreElement)BaseGet(Name);
            }
        }

        public int IndexOf(LinkToIgnoreElement path)
        {
            return BaseIndexOf(path);
        }

        public void Add(LinkToIgnoreElement path)
        {
            BaseAdd(path);
            // Add custom code here.
        }

        protected override void
            BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(LinkToIgnoreElement path)
        {
            if (BaseIndexOf(path) >= 0)
                BaseRemove(path.Path);
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
