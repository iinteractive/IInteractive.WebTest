using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class NetworkCredentialsCollection : ConfigurationElementCollection
    {
        public NetworkCredentialsCollection()
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
            return new NetworkCredentialsElement();
        }

        protected override Object
            GetElementKey(ConfigurationElement element)
        {
            return ((NetworkCredentialsElement)element).UriPrefix;
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


        public NetworkCredentialsElement this[int index]
        {
            get
            {
                return (NetworkCredentialsElement)BaseGet(index);
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

        new public NetworkCredentialsElement this[string Name]
        {
            get
            {
                return (NetworkCredentialsElement)BaseGet(Name);
            }
        }

        public int IndexOf(NetworkCredentialsElement host)
        {
            return BaseIndexOf(host);
        }

        public void Add(NetworkCredentialsElement host)
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

        public void Remove(NetworkCredentialsElement host)
        {
            if (BaseIndexOf(host) >= 0)
                BaseRemove(host.UriPrefix);
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
