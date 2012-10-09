using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.WebTest
{
    /// <summary>
    /// The purpose of this class is to serve as an entry point into this 
    /// application for testing processes.
    /// </summary>
    public class WebSiteTestSuiteGenerator
    {
        public List<WebPage> Seeds { get; set; }
        public List<Browser> Browser { get; set; }

        public WebSiteTestSuiteGenerator(List<WebPage> Seeds, List<Browser> Browser)
        {
            this.Seeds = Seeds;
            this.Browser = Browser;
        }
    }
}
