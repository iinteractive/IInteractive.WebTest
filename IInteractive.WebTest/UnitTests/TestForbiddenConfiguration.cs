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
    public class TestForbiddenConfiguration
    {
        [TestMethod]
        public void TestForbiddenNotRequired()
        {
            // Tests its there and can be retrieved.
            var configContents = string.Format(Resources.ForbiddenConfig, TestCrawler.RemoteHost, TestCrawler.GetTestUrl("/"));
            var config = TestConfigurationSections.RetrieveConfig(configContents);
            var linkCheckerConfig = (LinkCheckerConfigSection) config.GetSection("linkCheckerConfig");

            var forbiddenCollection = linkCheckerConfig.Forbidden;
            Assert.AreEqual(1, forbiddenCollection.Count);

            Assert.AreEqual(TestCrawler.RemoteHost, forbiddenCollection[0].Host);

            // Tests its not there.
            config = TestConfigurationSections.RetrieveConfig(Resources.NotForbiddenConfig);
            linkCheckerConfig = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

            forbiddenCollection = linkCheckerConfig.Forbidden;
            Assert.AreEqual(0, forbiddenCollection.Count);
        }

        [TestMethod]
        public void TestForbiddenSeedValidation()
        {
            try
            {
                var configContents = string.Format(Resources.ForbiddenConfig, TestCrawler.LocalHost, TestCrawler.GetTestUrl("/"));
                var config = TestConfigurationSections.RetrieveConfig(configContents);
                var linkCheckerConfig = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");

                var forbiddenCollection = linkCheckerConfig.Forbidden;
                var host = forbiddenCollection[0].Host;
            }
            catch(Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ConfigurationErrorsException));
            }
        }
    }
}
