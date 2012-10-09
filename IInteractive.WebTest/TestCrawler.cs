using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace IInteractive.WebTest
{
    [TestClass]
    public class TestCrawler
    {
        public static int Port = 50713;


        [TestMethod]
        public void AbsoluteUrlTestsCaseA()
        {
            TestCrawlerMethod(GetTestUrl("/AbsoluteUrlTests/CaseA/Seed.aspx"), 2);
        }

        [TestMethod]
        public void AbsoluteUrlTestsCaseB()
        {
            TestCrawlerMethod(GetTestUrl("/AbsoluteUrlTests/CaseB/Seed.aspx"), 2);
        }

        [TestMethod]
        public void AbsoluteUrlTestsCaseC()
        {
            TestCrawlerMethod(GetTestUrl("/AbsoluteUrlTests/CaseC/Folder/Seed.aspx"), 2);
        }

        [TestMethod]
        public void BaseUrlTestsCaseA()
        {
            TestCrawlerMethod(GetTestUrl("/BaseUrlTests/CaseA/Seed.htm"), 2);
        }

        [TestMethod]
        public void BaseUrlTestsCaseB()
        {
            TestCrawlerMethod(GetTestUrl("/BaseUrlTests/CaseB/Seed.htm"), 2);
        }

        [TestMethod]
        public void BaseUrlTestsCaseC()
        {
            TestCrawlerMethod(GetTestUrl("/BaseUrlTests/CaseC/Folder/Seed.htm"), 2);
        }

        [TestMethod]
        public void RelativeUrlTestsCaseA()
        {
            TestCrawlerMethod(GetTestUrl("/RelativeUrlTests/CaseA/Seed.htm"), 2);
        }

        [TestMethod]
        public void RelativeUrlTestsCaseB()
        {
            TestCrawlerMethod(GetTestUrl("/RelativeUrlTests/CaseB/Seed.htm"), 2);
        }

        [TestMethod]
        public void RelativeUrlTestsCaseC()
        {
            TestCrawlerMethod(GetTestUrl("/RelativeUrlTests/CaseC/Folder/Seed.htm"), 2);
        }

        [TestMethod]
        public void CrawlBackTestsCaseA()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlBackTests/CaseA/Seed.htm"), 1);
        }

        [TestMethod]
        public void CrawlBackTestsCaseB()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlBackTests/CaseB/Seed.htm"), 2);
        }

        [TestMethod]
        public void CrawlBackTestsCaseC()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlBackTests/CaseC/Seed.htm"), 3);
        }

        [TestMethod]
        public void CrawlBackTestsCaseD()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlBackTests/CaseD/Seed.htm"), 4);
        }

        [TestMethod]
        public void CrawlFanOutTestsCaseA()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlFanOutTests/CaseA/Seed.htm"), 1);
        }

        [TestMethod]
        public void CrawlFanOutTestsCaseB()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlFanOutTests/CaseB/Seed.htm"), 3);
        }

        [TestMethod]
        public void CrawlFanOutTestsCaseC()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlFanOutTests/CaseC/Seed.htm"), 7);
        }

        [TestMethod]
        public void CrawlFanOutTestsCaseD()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlFanOutTests/CaseD/Seed.htm"), 15);
        }

        [TestMethod]
        public void CrawlTestsCaseA()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlTests/CaseA/Seed.htm"), 1);
        }

        [TestMethod]
        public void CrawlTestsCaseB()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlTests/CaseB/Seed.htm"), 2);
        }

        [TestMethod]
        public void CrawlTestsCaseC()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlTests/CaseC/Seed.htm"), 3);
        }

        [TestMethod]
        public void CrawlTestsCaseD()
        {
            TestCrawlerMethod(GetTestUrl("/CrawlTests/CaseD/Seed.htm"), 4);
        }

        [TestMethod]
        public void ErroredLinkedTestsCaseA()
        {
            TestCrawlerMethod(GetTestUrl("/ErroredLinkedTests/CaseA/Seed.htm"), 2);
        }

        [TestMethod]
        public void ErroredLinkedTestsCaseB()
        {
            TestCrawlerMethod(GetTestUrl("/ErroredLinkedTests/CaseB/Seed.htm"), 2);
        }

        [TestMethod]
        public void ErroredLinkedTestsCaseC()
        {
            TestCrawlerMethod(GetTestUrl("/ErroredLinkedTests/CaseC/Seed.htm"), 2);
        }

        [TestMethod]
        public void ErroredLinkedTestsCaseD()
        {
            TestCrawlerMethod(GetTestUrl("/ErroredLinkedTests/CaseD/Seed.htm"), 2);
        }

        [TestMethod]
        public void ErroredLinkedTestsCaseE()
        {
            TestCrawlerMethod(GetTestUrl("/ErroredLinkedTests/CaseE/Seed.htm"), 2);
        }

        [TestMethod]
        public void ErroredSeedTestsCaseA()
        {
            TestCrawlerMethod(GetTestUrl("/ErroredSeedTests/CaseA/Seed.aspx"), 1);
        }

        [TestMethod]
        public void ErroredSeedTestsCaseB()
        {
            TestCrawlerMethod(GetTestUrl("/ErroredSeedTests/CaseB/Seed.aspx"), 1);
        }

        [TestMethod]
        public void ErroredSeedTestsCaseC()
        {
            TestCrawlerMethod(GetTestUrl("/ErroredSeedTests/CaseC/Seed.aspx"), 1);
        }

        [TestMethod]
        public void ErroredSeedTestsCaseD()
        {
            TestCrawlerMethod(GetTestUrl("/ErroredSeedTests/CaseD/Seed.aspx"), 1);
        }

        [TestMethod]
        public void ErroredSeedTestsCaseE()
        {
            TestCrawlerMethod(GetTestUrl("/ErroredSeedTests/CaseE/Seed.aspx"), 1);
        }

        [TestMethod]
        public void RemoteSiteCrawlTestsCaseA()
        {
            TestCrawlerMethod(GetTestUrl("/RemoteSiteCrawlTests/CaseA/Seed.htm"), 2);
        }

        [TestMethod]
        public void RemoteSiteCrawlTestsCaseB()
        {
            TestCrawlerMethod(GetTestUrl("/RemoteSiteCrawlTests/CaseB/Seed.htm"), 2);
        }

        [TestMethod]
        public void PerformanceTestCaseA()
        {
            TestCrawlerPerformance(2, 7, 35000, 21310, 256000);
        }

        [TestMethod]
        public void TestRecursionLimitCaseA()
        {
            TestCrawlerMethod(GetPerformanceTestUrl(2, 7, 10000, 12192), 1, 1);
        }

        [TestMethod]
        public void TestRecursionLimitCaseB()
        {
            TestCrawlerMethod(GetPerformanceTestUrl(2, 7, 10000, 12192), 2, 2);
        }

        [TestMethod]
        public void TestRecursionLimitCaseC()
        {
            TestCrawlerMethod(GetPerformanceTestUrl(2, 7, 10000, 12192), 3, 3);
        }

        private string GetTestUrl(string path)
        {
            return GetTestServer() + path;
        }

        private string GetTestServer() {
            return "http://127.0.0.1:" + Port;
        }

        private string GetPerformanceTestUrl(int fanOut, int depth, int data, int seed)
        {
            return GetTestUrl("/PerformanceTests/File-1?Depth=" + depth + "&FanOut=" + fanOut + "&Data=" + data + "&Seed=" + seed);
        }

        private void TestCrawlerPerformance(int fanOut, int depth, int data, int seed, long acceptablePerformance)
        {
            string path = GetPerformanceTestUrl(fanOut, depth, data, seed);
            TestCrawlerPerformanceGeneric(path, acceptablePerformance);
        }

        private void TestCrawlerPerformanceGeneric(string path, long acceptablePerformance)
        {

            List<string> uriList = new List<String>();
            uriList.Add(path);
            Crawler crawler = new Crawler(uriList, new Browser(), 500);

            Stopwatch watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            crawler.Crawl();
            watch.Stop();

            Console.WriteLine("Elapsed Milliseconds: " + watch.ElapsedMilliseconds);
            Console.WriteLine("crawler.Pages.Count(): " + crawler.Pages.Count());
            foreach (WebPage page in crawler.Pages)
            {
                if (page != null)
                {
                    Console.Write(page.RequestUrl + " : HttpCode = ");
                    if (page.Error != null)
                        Console.Write(page.Error.HttpCode);
                    else
                        Console.Write("200");

                    Console.WriteLine();
                }
                else
                    Console.WriteLine("Found null page.");
            }

            Assert.IsTrue(watch.ElapsedMilliseconds <= acceptablePerformance);
        }

        private void TestCrawlerMethod(string path, int expectedCount)
        {
            TestCrawlerMethod(path, expectedCount, 500);
        }

        private void TestCrawlerMethod(string path, int expectedCount, int recursionLimit)
        {
            List<string> uriList = new List<String>();
            uriList.Add(path);

            Crawler crawler = new Crawler(uriList, new Browser(), recursionLimit);

            crawler.Crawl();

            Assert.AreEqual(crawler.Pages.Count, expectedCount);
        }
    }
}
