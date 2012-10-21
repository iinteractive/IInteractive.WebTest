using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebTest.Properties;
using System.Configuration;
using IInteractive.WebConsole;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestBrowser
    {
        [TestMethod]
        public void TestRemoteAutomaticRedirectsCaseA()
        {
            var seedUrl = TestCrawler.GetTestUrl("/RemoteSiteRedirectTests/CaseA/R1.aspx");
            var remoteUrl = TestCrawler.GetRemoteTestUrl("/RemoteSiteRedirectTests/RemoteSite/CaseA/R1.aspx");
            var config = TestConfigurationSections.RetrieveConfig(Resources.CaseA);
            var section = (LinkCheckerConfigSection) config.GetSection("linkCheckerConfig");

            var browser = new Browser(section.Browsers[0]);
            var result = browser.Get(new Uri(seedUrl), false);
            var remoteResult = browser.Get(new Uri(remoteUrl), true);

            Assert.IsNotNull(result.Error, "ErrorCode = " + result.Error.Error);
            Assert.IsNull(remoteResult.Error);
        }

        [TestMethod]
        public void TestRemoteAutomaticRedirectsCaseC()
        {
            var seedUrl = TestCrawler.GetTestUrl("/RemoteSiteRedirectTests/CaseA/R1.aspx");
            var remoteUrl = TestCrawler.GetRemoteTestUrl("/RemoteSiteRedirectTests/RemoteSite/CaseA/R1.aspx");
            var config = TestConfigurationSections.RetrieveConfig(Resources.CaseC);
            var section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

            var browser = new Browser(section.Browsers[0]);
            var result = browser.Get(new Uri(seedUrl), false);
            var remoteResult = browser.Get(new Uri(remoteUrl), true);

            Assert.IsNotNull(result.Error);
            Assert.IsNotNull(remoteResult.Error);
        }

        [TestMethod]
        public void TestTimeoutCaseD()
        {
            var seedUrl = TestCrawler.GetTestUrl("/ErroredSeedTests/CaseF/seed.aspx");
            var config = TestConfigurationSections.RetrieveConfig(Resources.CaseD);
            var section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

            var browser = new Browser(section.Browsers[0]);
            var result = browser.Get(new Uri(seedUrl), false);

            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public void TestTimeoutCaseE()
        {
            var seedUrl = TestCrawler.GetTestUrl("/ErroredSeedTests/CaseF/seed.aspx");
            var config = TestConfigurationSections.RetrieveConfig(Resources.CaseE);
            var section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

            var browser = new Browser(section.Browsers[0]);
            var result = browser.Get(new Uri(seedUrl), false);

            Assert.IsNotNull(result.Error);
        }
    }
}
