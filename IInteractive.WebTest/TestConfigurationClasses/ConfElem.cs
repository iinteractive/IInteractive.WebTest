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
        [CallbackValidator(Type = typeof(int), CallbackMethodName="ValidateElement")]
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

        public void ValidateElement(object value)
        {
            if (value is int && ((int)value) != 2)
                return;
            else
                throw new ArgumentException("Just some validation error.", "value");
        }
    }
}
