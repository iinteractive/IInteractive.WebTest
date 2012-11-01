using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebConsole;
using IInteractive.WebTest.Properties;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestWebSiteTestSuiteGenerator
    {
        [TestMethod]
        public void TestForbiddenCaseA()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseA/Seed.aspx"), 1, 0, 0, 2, 4);
        }

        [TestMethod]
        public void TestForbiddenCaseB()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseB/Seed.aspx"), 2, 0, 0, 2, 5);
        }

        [TestMethod]
        public void TestForbiddenCaseC()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseC/Seed.aspx"), 1, 0, 0, 3, 5);
        }

        [TestMethod]
        public void TestForbiddenCaseD()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseD/Seed.aspx"), 1, 0, 0, 0, 2);
        }

        [TestMethod]
        public void TestForbiddenCaseE()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseE/Seed.aspx"), 0, 1, 0, 0, 2);
        }
        [TestMethod]
        public void TestForbiddenCaseF()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseF/Seed.aspx"), 0, 0, 1, 0, 2);
        }

        public void ForbiddenTestTemplate(string url, int expectedForbidden, int expectedBroken, int expectedForbiddenAndBroken, int expectedGood, int expectedCount)
        {
            string contents = string.Format(Resources.ForbiddenConfig, TestCrawler.RemoteHost, url);
            var config = TestConfigurationSections.RetrieveConfig(contents);
            var section = (LinkCheckerConfigSection)config.Sections.Get("linkCheckerConfig");
            Console.WriteLine(section.Timeout);
            var generator = new WebSiteTestSuiteGenerator(section, false, false);
            generator.GenerateTests();

            Assert.AreEqual(1, generator.Crawlers.Count);
            int isForbidden = 0;
            int isBroken = 0;
            int isForbiddenAndBroken = 0;
            int isGood = 0;
            foreach (var result in generator.Crawlers[0].HttpRequestResults)
            {
                if (result.Links != null)
                {
                    foreach (var link in result.Links)
                    {
                        if (link.IsBroken && link.IsForbidden)
                            isForbiddenAndBroken++;
                        else if (link.IsBroken && !link.IsForbidden)
                            isBroken++;
                        else if (!link.IsBroken && link.IsForbidden)
                            isForbidden++;
                        else
                            isGood++;
                    }
                }
            }

            Assert.AreEqual(expectedCount, generator.Crawlers[0].HttpRequestResults.Count);
            Assert.AreEqual(expectedForbidden, isForbidden);
            Assert.AreEqual(expectedBroken, isBroken);
            Assert.AreEqual(expectedForbiddenAndBroken, isForbiddenAndBroken);
            Assert.AreEqual(expectedGood, isGood);
        }

        public void TestIgnoredCaseA()
        {
            BrokenLinkTestTemplate("uri", "true", 1, 0, 1, 1, 4);
        }

        public void TestIgnoredCaseB()
        {
            BrokenLinkTestTemplate("path", "false", 1, 1, 0, 1, 4);
        }

        public void BrokenLinkTestTemplate(string pathsKey, string isUrisKey, int expectedBroken, int expectedIgnored, int expectedBrokenAndIgnored, int expectedGood, int expectedCount)
        {
            string contents = TestBrokenLinksConfigurations.GetConfigContentsFromKeys(pathsKey, isUrisKey);
            var config = TestConfigurationSections.RetrieveConfig(contents);
            var section = (LinkCheckerConfigSection)config.Sections.Get("linkCheckerConfig");
            Console.WriteLine(section.Timeout);
            var generator = new WebSiteTestSuiteGenerator(section, false, false);
            generator.GenerateTests();

            Assert.AreEqual(1, generator.Crawlers.Count);
            int isIgnored = 0;
            int isBroken = 0;
            int isIgnoredAndBroken = 0;
            int isGood = 0;
            foreach (var result in generator.Crawlers[0].HttpRequestResults)
            {
                if (result.Links != null)
                {
                    foreach (var link in result.Links)
                    {
                        if (link.IsBroken && link.IsIgnored)
                            isIgnoredAndBroken++;
                        else if (link.IsBroken && !link.IsIgnored)
                            isBroken++;
                        else if (!link.IsBroken && link.IsIgnored)
                            isIgnored++;
                        else
                            isGood++;
                    }
                }
            }

            Assert.AreEqual(expectedCount, generator.Crawlers[0].HttpRequestResults.Count);
            Assert.AreEqual(expectedIgnored, isIgnored);
            Assert.AreEqual(expectedBroken, isBroken);
            Assert.AreEqual(expectedBrokenAndIgnored, isIgnoredAndBroken);
            Assert.AreEqual(expectedGood, isGood);
        }
    }
}
