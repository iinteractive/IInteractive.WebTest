using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.WebTest
{
    /// <summary>
    /// The purpose of this class is to serve as an entry point into this 
    /// application for testing processes.
    /// 
    /// The bamboo integration would involve the use of a run configuration 
    /// file which would match the app.config file.
    /// </summary>
    public class WebSiteTestSuiteGenerator
    {
        public WebSiteTestSuiteGenerator(LinkCheckerConfigSection Config)
        {
            this.Config = Config;
        }

        public LinkCheckerConfigSection Config { get; set; }
        private List<Crawler> Crawlers { get; set; }
    }
}
