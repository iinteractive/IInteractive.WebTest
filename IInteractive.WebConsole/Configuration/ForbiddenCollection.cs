using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class ForbiddenCollection : ConfigurationElementCollection
    {
        public ForbiddenCollection()
        {
            
        }

        public static explicit operator List<String>(ForbiddenCollection collection)
        {
            var list = new List<String>();
            foreach (ForbiddenElement seed in collection)
            {
                list.Add(seed.Host);
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
            return new ForbiddenElement();
        }


        protected override
            ConfigurationElement CreateNewElement(
            string elementName)
        {
            return new ForbiddenElement(elementName);
        }


        protected override Object
            GetElementKey(ConfigurationElement element)
        {
            return ((ForbiddenElement)element).Host;
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


        public ForbiddenElement this[int index]
        {
            get
            {
                return (ForbiddenElement)BaseGet(index);
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

        new public ForbiddenElement this[string Name]
        {
            get
            {
                return (ForbiddenElement)BaseGet(Name);
            }
        }

        public int IndexOf(ForbiddenElement host)
        {
            return BaseIndexOf(host);
        }

        public void Add(ForbiddenElement host)
        {
            BaseAdd(host);
            // Add custom code here.
        }

        protected override void
            BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(ForbiddenElement host)
        {
            if (BaseIndexOf(host) >= 0)
                BaseRemove(host.Host);
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
