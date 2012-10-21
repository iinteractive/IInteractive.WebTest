using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebTest
{
    public class ConfSec : ConfigurationSection
    {
        [ConfigurationProperty("confColl",
            IsDefaultCollection = false, IsRequired = false)]
        public ConfColl ConfColl
        {
            get
            {
                ConfColl urlsCollection = (ConfColl)base["confColl"];
                if (urlsCollection == null)
                    urlsCollection = new ConfColl();

                if(urlsCollection != null)
                    urlsCollection.Parent = this;

                return urlsCollection;
            }
        }

        [ConfigurationProperty("prop", DefaultValue = "2")]
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
    }
}
