using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebTest
{
    public class ConfElem : ConfigurationElement
    {
        public ConfColl Parent;

        public ConfElem() : base()
        {
        }

        public ConfElem(string Name)
            : base()
        {
            this.Name = Name;
        }

        public int GlobalProp
        {
            get
            {
                return Parent.Parent.Prop;
            }
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

        [ConfigurationProperty("prop", IsRequired = true, DefaultValue = 1, IsKey = true)]
        public int Prop
        {
            get
            {
                return (int)this["prop"];
            }
            set
            {
                this["prop"] = value;
            }
        }

        [ConfigurationProperty("callback", IsRequired = false, DefaultValue = 1)]
        public int Callback
        {
            get
            {
                return (int)this["callback"];
            }
            set
            {
                this["callback"] = value;
            }
        }

        protected override void PostDeserialize()
        {
            base.PostDeserialize();
            if (Callback != 1)
                throw new ArgumentException("What?!?!");
        }
    }
}
