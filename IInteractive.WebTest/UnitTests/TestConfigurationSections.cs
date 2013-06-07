using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using IInteractive.WebConsole;
using System.Xml.Serialization;
using System.IO;
using IInteractive.WebTest.Properties;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestConfigurationSections
    {
        [TestMethod]
        public void TestBrowserDefaultConfig()
        {
            Browser browser = new Browser();
            Assert.AreNotEqual(default(string), browser.Accept);
            Console.WriteLine(browser.Accept);
            Assert.AreNotEqual(default(string), browser.AcceptCharset);
            Assert.AreNotEqual(default(string), browser.AcceptLanguage);
        }

        public BrowserCollection AssertNotNull(Configuration config)
        {
            ConfigurationSectionCollection sections = config.Sections;
            Assert.IsNotNull(sections);
            ConfigurationSection section = sections.Get("linkCheckerConfig");
            Assert.IsNotNull(section);
            LinkCheckerConfigSection linkSection = (LinkCheckerConfigSection)section;
            Assert.IsNotNull(linkSection);
            Assert.IsNotNull(linkSection.RecursionLimit);
            Assert.IsNotNull(linkSection.TimeLimit);
            Assert.IsNotNull(linkSection.Timeout);
            BrowserCollection browsers = linkSection.Browsers;
            Assert.IsNotNull(browsers);

            return browsers;
        }

        [TestMethod]
        public void TestLinkCheckerConfigSection()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            BrowserCollection browsers = AssertNotNull(config);

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

            Assert.AreEqual(2, ctr);
        }

        public static string GetNormalConfig()
        {
            return WriteLine(ConfigTmpl,
                    WriteLine(ConfigSectionsTmpl,
                        ConfigSectionsDefault
                    )
                        +
                    WriteLine(LinkCheckerConfigTmpl,
                        LinkCheckConfigDefaultAttr,
                        WriteLine(BrowsersTmpl,
                            BrowserDefault
                        )
                            +
                        WriteLine(SeedsTmpl,
                            SeedDefault
                        )
                    )
                );
        }

        [TestMethod]
        public void TestNormalConfig()
        {
            string value
                = GetNormalConfig();
            TemplateMethod(value, false);
        }

        public static string GetNoLinkCheckerAttrConfig()
        {
            return WriteLine(ConfigTmpl,
                    WriteLine(ConfigSectionsTmpl,
                        ConfigSectionsDefault
                    )
                        +
                    WriteLine(LinkCheckerConfigTmpl,
                        "",
                        WriteLine(BrowsersTmpl,
                            BrowserDefault
                        )
                            +
                        WriteLine(SeedsTmpl,
                            SeedDefault
                        )
                    )
                );
        }

        [TestMethod]
        public void TestNoLinkCheckerAttrConfig()
        {
            string value
                = GetNoLinkCheckerAttrConfig();
            TemplateMethod(value, false);
        }

        public static string GetNoSeedsConfig()
        {
            return WriteLine(ConfigTmpl,
                    WriteLine(ConfigSectionsTmpl,
                        ConfigSectionsDefault
                    )
                        +
                    WriteLine(LinkCheckerConfigTmpl,
                        LinkCheckConfigDefaultAttr,
                        WriteLine(BrowsersTmpl,
                            BrowserDefault
                        )
                    )
                );
        }

        [TestMethod]
        public void TestNoSeedsConfig()
        {
            string value
                = GetNoSeedsConfig();
            TemplateMethod(value, true);
        }

        public static string GetNoBrowsersConfig() {
            return WriteLine(ConfigTmpl,
                    WriteLine(ConfigSectionsTmpl,
                        ConfigSectionsDefault
                    )
                        +
                    WriteLine(LinkCheckerConfigTmpl,
                        LinkCheckConfigDefaultAttr,
                        WriteLine(SeedsTmpl,
                            SeedDefault
                        )
                    )
                );
        }

        [TestMethod]
        public void TestNoBrowsersConfig()
        {
            string value
                = GetNoBrowsersConfig();
            TemplateMethod(value, false);
        }

        public static string GetNoConfigSections()
        {
            return WriteLine(ConfigTmpl,
                    WriteLine(LinkCheckerConfigTmpl,
                        LinkCheckConfigDefaultAttr,
                        WriteLine(BrowsersTmpl,
                            BrowserDefault
                        )
                            +
                        WriteLine(SeedsTmpl,
                            SeedDefault
                        )
                    )
                );
        }

        [TestMethod]
        public void TestNoConfigSections()
        {
            string value
                = GetNoConfigSections();
            try
            {
                TemplateMethod(value, true);
                Assert.IsTrue(false);
            }
            catch (InvalidCastException)
            {

            }
        }

        private void TemplateMethod(string contents, bool expectedConfErrorException)
        {
            try
            {
                AssertNotNull(RetrieveConfig(contents));
                Assert.IsFalse(expectedConfErrorException);
            }
            catch(ConfigurationErrorsException) 
            {
                Assert.IsTrue(expectedConfErrorException);
            }
        }

        private void TestValidationAssumption(Configuration config)
        {
            LinkCheckerConfigSection section = (LinkCheckerConfigSection) config.GetSection("linkCheckerConfig");
            Assert.IsNotNull(section);
            BrowserCollection collection = section.Browsers;
            Assert.IsNotNull(collection);

        }

        public static Configuration RetrieveConfig(string contents)
        {
            string fileName = Path.GetTempFileName();
            Console.WriteLine("fileName = {0}", fileName);
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(contents);
                writer.Flush();
                writer.Close();
            }

            Console.WriteLine("CONTENTS-------------------------\n{0}\n/CONTENTS------------------------", contents);
            using(StreamReader reader = new StreamReader(fileName))
            {
                string fileContents = reader.ReadToEnd();
                Console.WriteLine("FILE CONTENTS-------------------------\n{0}\n/FILE CONTENTS------------------------", fileContents);
            }
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = fileName;
            return ConfigurationManager.OpenMappedExeConfiguration(
                             map
                             , ConfigurationUserLevel.None
                         );
        }

        private static string ConfigTmpl = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<configuration>\n{0}\n</configuration>";
        private static string ConfigSectionsTmpl = "<configSections>\n{0}\n</configSections>";
        private static string LinkCheckerConfigTmpl = "<linkCheckerConfig {0}>\n{1}\n</linkCheckerConfig>";
        private static string BrowsersTmpl = "<browsers>\n<clear/>\n{0}\n</browsers>";
        private static string SeedsTmpl = "<seeds>\n<clear/>\n{0}\n</seeds>";

        private static string ConfigSectionsDefault = "<section name=\"linkCheckerConfig\" type=\"IInteractive.WebConsole.LinkCheckerConfigSection, IInteractive.WebConsole\" allowDefinition=\"Everywhere\" allowLocation=\"true\" />";
        private static string LinkCheckConfigDefaultAttr = "name=\"This is the name!\" description=\"This is the description!\" recursionLimit=\"5000\" timeout=\"60\" timeLimit=\"21600\" testResultsFile=\"output.trx\"";
        private static string BrowserDefault = "<add"
            + "\n\tname=\"default\""
            + "\n\tmaximumAutomaticRedirections=\"2\""
            + "\n\tallowAutoRedirect=\"true\""
            + "\n\tuserAgent=\"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.79 Safari/537.4\""
            + "\n\taccept=\"*/*\""
            + "\n\tacceptCharset=\"ISO-8859-1,utf-8;q=0.7,*;q=0.3\""
            + "\n\tacceptLanguage=\"en-US,en;q=0.8\" />";
        private static string SeedDefault = "<add uri=\"http://webcrawlertest:80/Index.aspx\"/>";

        private static string Write(string val, params Object[] args)
        {
            if (args.Length == 1)
            {
                var writer = new StringWriter();
                writer.Write(val, args[0]);
                return writer.ToString();
            }
            else if (args.Length == 2)
            {
                var writer = new StringWriter();
                writer.Write(val, args[0], args[1]);
                return writer.ToString();
            }
            else if (args.Length == 3)
            {
                var writer = new StringWriter();
                writer.Write(val, args[0], args[1], args[2]);
                return writer.ToString();
            }
            else return null;
        }

        private static string WriteLine(string val, params Object[] args)
        {
            if (args.Length == 1)
            {
                var writer = new StringWriter();
                writer.WriteLine(val, args[0]);
                return writer.ToString();
            }
            else if (args.Length == 2)
            {
                var writer = new StringWriter();
                writer.WriteLine(val, args[0], args[1]);
                return writer.ToString();
            }
            else if (args.Length == 3)
            {
                var writer = new StringWriter();
                writer.WriteLine(val, args[0], args[1], args[2]);
                return writer.ToString();
            }
            else return null;
        }

    }


}
