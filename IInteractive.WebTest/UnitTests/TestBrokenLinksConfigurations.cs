using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using IInteractive.WebConsole;
using IInteractive.WebTest.Properties;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestBrokenLinksConfigurations
    {
        public static Dictionary<string, string> Paths = new Dictionary<string, string>()
        {
           {"uri", TestCrawler.GetTestUrl("/BrokenLinkConfigTests/NonSeed/BrokenLink1.aspx")},
           {"path", "http://{}[]!@#$%^&*()_+~`"}
        };

        public static Dictionary<string, string> IsUris = new Dictionary<string, string>()
        {
            {"default", ""},
            {"true", "isUri=\"true\""},
            {"false", "isUri=\"false\""},
            {"error", "isUri=\"cfdsafdsa\""}
        };

        [TestMethod]
        public void TestBrokenLinkConfigCaseA()
        {
            TemplateMethod("uri", "default", false, "uri", true);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseB()
        {
            TemplateMethod("uri", "true", false, "uri", true);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseC()
        {
            TemplateMethod("uri", "false", false, "uri", false);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseD()
        {
            TemplateMethod("uri", "error", false, "uri", false);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseE()
        {
            TemplateMethod("path", "default", false, "path", true);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseF()
        {
            TemplateMethod("path", "true", false, "path", true);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseG()
        {
            TemplateMethod("path", "false", false, "path", false);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseH()
        {
            TemplateMethod("path", "error", false, "path", false);
        }



        public void TemplateMethod(string pathsKey, string isUrisKey, bool exceptionExpected, string expectedPath, bool expectedIsUri)
        {
            string contents = GetConfigContents(Paths[pathsKey], IsUris[isUrisKey]);

            Configuration config = null;
            try
            {
                config = TestConfigurationSections.RetrieveConfig(contents);
                Assert.IsFalse(exceptionExpected);
            }
            catch (ConfigurationErrorsException)
            {
                Assert.IsTrue(exceptionExpected);
            }

            if (config != null)
            {
                var section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");
                var brokenLinks = section.LinksToIgnore;

                Assert.AreEqual(1, brokenLinks.Count);
                var brokenLinkConfig = brokenLinks[0];
                Assert.AreEqual(Paths[expectedPath], brokenLinkConfig.Path);
                Assert.AreEqual(expectedIsUri, brokenLinkConfig.IsUri);
            }
        }

        public static string GetConfigContentsFromKeys(string pathKey, string isUriAttrKey)
        {
            return GetConfigContents(Paths[pathKey], IsUris[isUriAttrKey]);
        }

        private static string GetConfigContents(string path, string isUriAttr)
        {
            return string.Format(Resources.BrokenLinksTemplate, path, isUriAttr
                , TestCrawler.GetTestUrl("/BrokenLinkConfigTests/CaseA/Seed.htm")
            );
        }
    }
}
