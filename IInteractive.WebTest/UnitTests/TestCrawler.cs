using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using IInteractive.WebConsole;

namespace IInteractive.WebTest
{
    [TestClass]
    public class TestCrawler
    {
        public static int Port = 80;

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
        public void UriFormatExceptionTestsCaseA()
        {
            string path = "http://{}[]!@#$%^&*()_+~`";
            Uri root = new Uri(GetTestUrl("/UriFormatExceptionTests/CaseA/Seed.htm"));
            try
            {
                Uri uri = new Uri(root, path);
                Console.WriteLine("uri.AbsoluteUri = " + uri.AbsoluteUri);
                Assert.IsTrue(false);
            }
            catch (UriFormatException)
            {

            }
            Crawler crawler = TestCrawlerMethod(GetTestUrl("/UriFormatExceptionTests/CaseA/Seed.htm"), 1);
            foreach (var httpRequestResult in crawler.HttpRequestResults)
            {
                Assert.IsNotNull(httpRequestResult.Links);
                Assert.AreEqual(1, httpRequestResult.Links.Count);
                Assert.IsNull(httpRequestResult.Links[0].AbsoluteUri);
                Assert.IsNotNull(httpRequestResult.Links[0].Ex);
            }
        }

        [TestMethod]
        public void PerformanceTestCaseA()
        {
            TestCrawlerPerformance(2, 7, 35000, 21310, 35000);
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
            return "http://webcrawlertest:" + Port;
        }

        private string GetPerformanceTestUrl(int fanOut, int depth, int data, int seed)
        {
            return GetTestUrl("/PerformanceTests/File-1?Depth=" + depth + "&FanOut=" + fanOut + "&Data=" + data + "&Seed=" + seed);
        }

        private void TestCrawlerPerformance(int fanOut, int depth, int data, int seed, int acceptablePerformance)
        {
            string path = GetPerformanceTestUrl(fanOut, depth, data, seed);
            TestCrawlerPerformanceGeneric(path, acceptablePerformance);
        }

        private void TestCrawlerPerformanceGeneric(string path, int acceptablePerformance)
        {

            List<string> uriList = new List<String>();
            uriList.Add(path);
            Crawler crawler = new Crawler(uriList, new Browser(), 500);
            Thread thread = new Thread(crawler.Crawl);

            TimeSpan acceptableTimeSpan = new TimeSpan(0, 0, 0, 0, acceptablePerformance);

            Stopwatch watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            thread.Start();
            thread.Join(acceptableTimeSpan);
            watch.Stop();

            Assert.IsFalse(thread.IsAlive);

            Console.WriteLine("Elapsed Milliseconds: " + watch.ElapsedMilliseconds);
            Console.WriteLine("crawler.Pages.Count(): " + crawler.HttpRequestResults.Count());
            foreach (var page in crawler.HttpRequestResults)
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

        private Crawler TestCrawlerMethod(string path, int expectedCount)
        {
            return TestCrawlerMethod(path, expectedCount, 500);
        }

        private Crawler TestCrawlerMethod(string path, int expectedCount, int recursionLimit)
        {
            List<string> uriList = new List<String>();
            uriList.Add(path);

            Crawler crawler = new Crawler(uriList, new Browser(), recursionLimit);

            crawler.Crawl();

            Assert.AreEqual(expectedCount, crawler.HttpRequestResults.Count);

            AssertLinksFromRemoteSiteNotRetrieved(crawler);
            AssertLinksNullStateForCssAndHtmlTypes(crawler);
            AssertBadLinksHaveNullAbsoluteUriAndPopulatedEx(crawler);

            return crawler;
        }

        private void AssertLinksFromRemoteSiteNotRetrieved(Crawler crawler)
        {
            foreach (var httpRequestResult in crawler.HttpRequestResults)
            {
                if (httpRequestResult.Links != null && !crawler.GetSetOfCrawlableHosts().Contains(httpRequestResult.ResultUrl.Host))
                {
                    foreach (var link in httpRequestResult.Links)
                    {
                        Assert.IsTrue(link.WasRetrieved == false);
                    }
                }
            }
        }

        private void AssertLinksNullStateForCssAndHtmlTypes(Crawler crawler)
        {
            foreach (var httpRequestResult in crawler.HttpRequestResults)
            {
                if (httpRequestResult.IsCss || httpRequestResult.IsHtml)
                {
                    Assert.IsNotNull(httpRequestResult.Links);
                }
                else
                {
                    Assert.IsNull(httpRequestResult.Links);
                }
            }
        }

        private void AssertBadLinksHaveNullAbsoluteUriAndPopulatedEx(Crawler crawler)
        {
            foreach (var httpRequestResult in crawler.HttpRequestResults)
            {
                if (httpRequestResult.Links != null)
                {
                    foreach (var link in httpRequestResult.Links)
                    {
                        Assert.IsTrue(link.AbsoluteUri == null && link.Ex != null || link.AbsoluteUri != null && link.Ex == null);
                        
                    }
                }
            }
        }


    }
}
