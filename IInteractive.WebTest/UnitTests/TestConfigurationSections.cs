using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestConfigurationSections
    {
        [TestMethod]
        public void TestLinkCheckerConfigSection()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSectionCollection sections = config.Sections;
            Assert.IsNotNull(sections);
            ConfigurationSection section = sections.Get("linkCheckerConfig");
            Assert.IsNotNull(section);
            LinkCheckerConfigSection linkSection = (LinkCheckerConfigSection)section;
            Assert.IsNotNull(linkSection);
            Assert.IsNotNull(linkSection.RecursionLimit);
            Assert.IsNotNull(linkSection.MaxCrawlTime);
            Assert.IsNotNull(linkSection.RequestTimeout);
            BrowserCollection browsers = linkSection.Browsers;
            Assert.IsNotNull(browsers["default"]);
            Assert.IsNotNull(browsers["default2"]);
            Assert.IsNotNull(browsers);

            BrowserConfigElement last = null;
            int ctr = 0;
            foreach (BrowserConfigElement browser in browsers)
            {
                Assert.IsNotNull(browser.Accept);
                Assert.IsNotNull(browser.AcceptCharset);
                Assert.IsNotNull(browser.AcceptLanguage);
                Assert.IsNotNull(browser.AllowAutoRedirect);
                Assert.IsNotNull(browser.UserAgent);
                Assert.IsNotNull(browser.MaximumAutomaticRedirections);

                if (ctr != 0)
                {
                    Assert.AreNotEqual(last.Name, browser.Name);
                    Assert.AreEqual(last.Accept, browser.Accept);
                    Assert.AreEqual(last.AcceptCharset, browser.AcceptCharset);
                    Assert.AreEqual(last.AcceptLanguage, browser.AcceptLanguage);
                    Assert.AreEqual(last.AllowAutoRedirect, browser.AllowAutoRedirect);
                    Assert.AreEqual(last.MaximumAutomaticRedirections, browser.MaximumAutomaticRedirections);
                    Assert.AreEqual(last.UserAgent, browser.UserAgent);
                }
                ctr++;
                last = browser;
            }

            Assert.AreEqual(7, ctr);
        }
    }


}
