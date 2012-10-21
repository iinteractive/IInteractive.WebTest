using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebTest
{
    public class ConfColl : ConfigurationElementCollection
    {
        public ConfSec Parent;

        public ConfColl() : base()
        {
            ConfElem first = new ConfElem();
            first.Parent = this;
            Add(first);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override
            ConfigurationElement CreateNewElement()
        {
            ConfElem elem = new ConfElem();
            elem.Parent = this;
            return elem;
        }


        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            ConfElem elem = new ConfElem(elementName);
            elem.Parent = this;
            return elem;
        }


        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ConfElem)element).Name;
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


        public ConfElem this[int index]
        {
            get
            {
                ConfElem elem = (ConfElem)BaseGet(index);
                if(elem != null)
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

        new public ConfElem this[string Name]
        {
            get
            {
                ConfElem elem = (ConfElem)BaseGet(Name);
                if(elem != null)
                    elem.Parent = this;
                return elem;
            }
        }

        public int IndexOf(ConfElem elem)
        {
            return BaseIndexOf(elem);
        }

        public void Add(ConfElem elem)
        {
            BaseAdd(elem);
            // Add custom code here.
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(ConfElem elem)
        {
            if (BaseIndexOf(elem) >= 0)
                BaseRemove(elem.Name);
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
