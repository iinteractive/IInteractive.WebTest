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
        public List<Crawler> Crawlers;

        public void GenerateTests()
        {
            var creationTimestamp = DateTime.Now;
            // Sets up crawlers.
            Crawlers = new List<Crawler>();
            for(int i = 0; i < Config.Browsers.Count; i++)
            {
                var browserConfig = Config.Browsers[i];
                Browser browser = new Browser(browserConfig, Config.NetworkCredentials);

                Crawler crawler = new Crawler(Config.Seeds, browser, Config.RecursionLimit, (List<string>) Config.Forbidden, Config.LinksToIgnore);
                Crawlers.Add(crawler);
            }

            // Start crawling.
            var results = new List<HttpRequestResult>();
            foreach (var crawler in Crawlers)
            {
                crawler.Crawl();
                results.AddRange(crawler.HttpRequestResults);
            }

            var startTime = DateTime.Now;

            // Creates the test results and writes them to a file.
            var file = new FileInfo(Config.TestResultsFile);
            if (file.Exists) file.Delete();
            var writer = new WebTestXmlWriter();

            var finishTimestamp = DateTime.Now;

            var testRun = (new TestResultsFactory()).GenerateTestRun(Config.Name,
                                                                     Config.Description,
                                                                     startTime,
                                                                     finishTimestamp,
                                                                     creationTimestamp,
                                                                     startTime,
                                                                     results);

            var streamWriter = file.CreateText();
            writer.Write(streamWriter, testRun);
            streamWriter.Flush();
            streamWriter.Close();
            
        }
    }
}
