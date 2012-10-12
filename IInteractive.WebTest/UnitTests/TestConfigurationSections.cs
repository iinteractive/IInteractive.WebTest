using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using IInteractive.WebConsole;
using System.Xml.Serialization;
using System.IO;

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

        [TestMethod]
        public void TestTemplateFileAccess()
        {
            
        }

        private static string ConfigTmpl = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><configuration>{0}</configuration>";
        private static string ConfigSectionsTmpl = "<configSections>\n{0}\n</configSections>";
        private static string ConfigSectionsDefault = "<section name=\"linkCheckerConfig\" type=\"IInteractive.WebConsole.LinkCheckerConfigSection, IInteractive.WebConsole\" allowDefinition=\"Everywhere\" allowLocation=\"true\" />";
        private static string LinkCheckerConfigTmpl = "<linkCheckerConfig {0}>\n{1}\n</linkCheckerConfig>";
        private static string LinkCheckConfigDefaultAttr = "recursionLimit=\"5000\" requestTimeout=\"60\" maxCrawlTime=\"21600\"";
        private static string BrowsersTmpl = "<browsers>\n<clear/>\n{0}\n</browsers>";
        private static string BrowserDefault = "<add"
            + "\n\tname=\"default\""
            + "\n\tmaximumAutomaticRedirections=\"2\""
            + "\n\tallowAutoRedirect=\"true\""
            + "\n\tuserAgent=\"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.79 Safari/537.4\""
            + "\n\taccept=\"*/*\""
            + "\n\tacceptCharset=\"ISO-8859-1,utf-8;q=0.7,*;q=0.3\""
            + "\n\tacceptLanguage=\"en-US,en;q=0.8\" />";
        private static string SeedsTmpl = "<browsers><clear/>\n{0}\n</browsers>";
        private static string SeedDefault = "<add uri=\"http://127.0.0.1/\"/>";

        public string ConstructConfig()
        {
            
        }
    }


}
