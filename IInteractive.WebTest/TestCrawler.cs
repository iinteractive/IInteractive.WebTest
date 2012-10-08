using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IInteractive.WebTest
{
    [TestClass]
    public class TestCrawler
    {
        [TestMethod]
        public void TestCrawlerClosure()
        {
            string uri1 = "http://localhost:50349/index.htm";
            string uri2 = "http://localhost:50349/folder/page.htm";
            List<string> uri1List = new List<String>();
            uri1List.Add(uri1);
            List<string> uri2List = new List<String>();
            uri2List.Add(uri2);
            List<string> bothList = new List<String>();
            bothList.Add(uri1);
            bothList.Add(uri2);
            Crawler crawler1 = new Crawler(uri1List, new Browser());
            Crawler crawler2 = new Crawler(uri2List, new Browser());
            Crawler crawler3 = new Crawler(bothList, new Browser());

            crawler1.Crawl();
            Assert.AreEqual(crawler1.Pages.Count, 2);
            crawler2.Crawl();
            Assert.AreEqual(crawler2.Pages.Count, 2);
            crawler3.Crawl();
            Assert.AreEqual(crawler3.Pages.Count, 2);
        }

        [TestMethod]
        public void TestPerformance()
        {
            string uri1 = "http://www.aetnamedicare.com";
            List<string> uri1List = new List<String>();
            uri1List.Add(uri1);
            Crawler crawler1 = new Crawler(uri1List, new Browser());

            crawler1.Crawl();
            Assert.AreEqual(crawler1.Pages.Count, 2);
        }
    }
}
