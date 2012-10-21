using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebTest.Properties;
using IInteractive.WebConsole;
using System.Configuration;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestConfigurationParenting
    {


        [TestMethod]
        public void TestBrowsersParentingNullCheck()
        {
            Configuration config = TestConfigurationSections.RetrieveConfig(Resources.CaseA);
            LinkCheckerConfigSection section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

            Assert.IsNotNull(section.Browsers.Parent);
            Assert.IsNotNull(section.Browsers[0].Parent);
            Assert.IsTrue(section.Browsers.Parent == section);
            Assert.IsTrue(section.Browsers == section.Browsers[0].Parent);
        }

        [TestMethod]
        public void TestBrowsersParentingCaseA()
        {
            Configuration config = TestConfigurationSections.RetrieveConfig(Resources.CaseA);
            LinkCheckerConfigSection section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

            Assert.AreEqual(10, section.MaxRemoteAutomaticRedirects);
            Assert.AreEqual(10, section.Browsers[0].MaxRemoteAutomaticRedirects);
            Assert.AreEqual(2, section.Browsers[0].MaximumAutomaticRedirections);
        }

        [TestMethod]
        public void TestBrowsersParentingCaseB()
        {
            Configuration config = TestConfigurationSections.RetrieveConfig(Resources.CaseB);
            LinkCheckerConfigSection section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

            Assert.AreEqual(10, section.MaxRemoteAutomaticRedirects);
            Assert.AreEqual(10, section.Browsers[0].MaxRemoteAutomaticRedirects);
            Assert.AreEqual(10, section.Browsers[0].MaximumAutomaticRedirections);
        }

        [TestMethod]
        public void TestBrowsersParentingCaseC()
        {
            Configuration config = TestConfigurationSections.RetrieveConfig(Resources.CaseC);
            LinkCheckerConfigSection section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

            Assert.AreEqual(2, section.MaxRemoteAutomaticRedirects);
            Assert.AreEqual(2, section.Browsers[0].MaxRemoteAutomaticRedirects);
            Assert.AreEqual(2, section.Browsers[0].MaximumAutomaticRedirections);
        }

        [TestMethod]
        public void TestBrowsersParentingCaseD()
        {
            Configuration config = TestConfigurationSections.RetrieveConfig(Resources.CaseD);
            LinkCheckerConfigSection section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

            Assert.AreEqual(60, section.Timeout);
            Assert.AreEqual(60, section.Browsers[0].Timeout);
            Assert.AreEqual(60, section.Browsers[0].Timeout);
        }

        [TestMethod]
        public void TestBrowsersParentingCaseE()
        {
            Configuration config = TestConfigurationSections.RetrieveConfig(Resources.CaseE);
            LinkCheckerConfigSection section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

            Assert.AreEqual(50, section.Timeout);
            Assert.AreEqual(50, section.Browsers[0].Timeout);
            Assert.AreEqual(50, section.Browsers[0].Timeout);
        }

        
    }
}
