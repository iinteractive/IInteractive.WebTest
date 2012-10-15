using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IInteractive.WebConsole.Results;
using System.IO;

namespace IInteractive.WebConsole
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

        public void GenerateTests()
        {
            // Sets up crawlers.
            List<Crawler> crawlers = new List<Crawler>();
            foreach (BrowserConfigElement browserConfig in Config.Browsers)
            {
                Browser browser = new Browser(browserConfig, Config.NetworkCredentials);

                Crawler crawler = new Crawler(Config.Seeds, browser, Config.RecursionLimit);
                crawlers.Add(crawler);
            }

            // Start crawling.
            var results = new List<HttpRequestResult>();
            foreach (var crawler in crawlers)
            {
                crawler.Crawl();
                results.AddRange(crawler.HttpRequestResults);
            }

            // Creates the test results and writes them to a file.
            var file = new FileInfo(Config.TestResultsFile);
            if (file.Exists) file.Delete();
            var writer = new WebTestXmlWriter();
            var testRun = (new TestResultsFactory()).GenerateTestRun(Config.Name, Config.Description, DateTime.Now,
                                                                     DateTime.Now.Add(new TimeSpan(0, 0, 23)),
                                                                     DateTime.Now, DateTime.Now,
                                                                     results);

            writer.Write(file.CreateText(), testRun);
        }
    }
}
