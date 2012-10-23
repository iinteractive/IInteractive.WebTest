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
           {"path", "http://{}[]!@#$%^*()_+~`"}
        };

        public static Dictionary<string, string> IsUris = new Dictionary<string, string>()
        {
            {"default", ""},
            {"true", "isUri=\"true\""},
            {"false", "isUri=\"false\""},
            {"error", "isUri=\"cfdsafdsa\""}
        };

        [TestMethod]
        public void TestIsUriFormatException()
        {
            try
            {
                new Uri(Paths["path"]);
                Assert.IsFalse(true);
            }
            catch(UriFormatException)
            {
            }
        }

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
            TemplateMethod("uri", "error", true, "uri", false);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseE()
        {
            TemplateMethod("path", "default", true, "path", true);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseF()
        {
            TemplateMethod("path", "true", true, "path", true);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseG()
        {
            TemplateMethod("path", "false", false, "path", false);
        }

        [TestMethod]
        public void TestBrokenLinkConfigCaseH()
        {
            TemplateMethod("path", "error", true, "path", false);
        }



        public void TemplateMethod(string pathsKey, string isUrisKey, bool exceptionExpected, string expectedPath, bool expectedIsUri)
        {
            string contents = GetConfigContents(Paths[pathsKey], IsUris[isUrisKey]);

            Configuration config = null;
            LinkCheckerConfigSection section = null;
            try
            {
                config = TestConfigurationSections.RetrieveConfig(contents);
                section = (LinkCheckerConfigSection)config.GetSection("linkCheckerConfig");
                Assert.IsFalse(exceptionExpected);
            }
            catch (ConfigurationErrorsException ex)
            {
                if (!exceptionExpected)
                    Console.WriteLine(ex);
                Assert.IsTrue(exceptionExpected);
            }

            if (section != null)
            {
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
