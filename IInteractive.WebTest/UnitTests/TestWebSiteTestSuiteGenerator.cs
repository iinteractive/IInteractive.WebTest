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
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseA/Seed.aspx"), 1, 0, 0, 2);
        }

        [TestMethod]
        public void TestForbiddenCaseB()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseB/Seed.aspx"), 2, 0, 0, 2);
        }

        [TestMethod]
        public void TestForbiddenCaseC()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseC/Seed.aspx"), 1, 0, 0, 3);
        }

        [TestMethod]
        public void TestForbiddenCaseD()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseD/Seed.aspx"), 1, 0, 0, 1);
        }

        [TestMethod]
        public void TestForbiddenCaseE()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseE/Seed.aspx"), 0, 1, 0, 0);
        }
        [TestMethod]
        public void TestForbiddenCaseF()
        {
            ForbiddenTestTemplate(TestCrawler.GetTestUrl("/ForbiddenTests/CaseE/Seed.aspx"), 0, 0, 1, 0);
        }


        public void ForbiddenTestTemplate(string url, int expectedForbidden, int expectedBroken, int expectedForbiddenAndBroken, int expectedGood)
        {
            string contents = string.Format(Resources.ForbiddenConfig, TestCrawler.RemoteHost, url);
            var config = TestConfigurationSections.RetrieveConfig(contents);
            var section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");
            var generator = new WebSiteTestSuiteGenerator(section);
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

            Assert.AreEqual(expectedForbidden, isForbidden);
            Assert.AreEqual(expectedBroken, isBroken);
            Assert.AreEqual(expectedForbiddenAndBroken, isForbiddenAndBroken);
            Assert.AreEqual(expectedGood, isGood);
        }
    }
}
