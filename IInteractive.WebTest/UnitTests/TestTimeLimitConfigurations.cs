using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using IInteractive.WebTest.Properties;
using IInteractive.WebConsole;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestTimeLimitConfigurations
    {
        public static Dictionary<string, string> TimeLimits = new Dictionary<string, string>()
        {
           {"fail", "timeLimit=\"5\""},
           {"pass", "timeLimit=\"20\""},
           {"default", ""},
           {"error", "timeLimit=\"-2\""}
        };

        [TestMethod]
        public void TestTimeLimitConfigurationCaseA()
        {
            TemplateMethod("fail", false, 5);
        }

        [TestMethod]
        public void TestTimeLimitConfigurationCaseB()
        {
            TemplateMethod("pass", false, 20);
        }

        [TestMethod]
        public void TestTimeLimitConfigurationCaseC()
        {
            TemplateMethod("default", false, -1);
        }

        [TestMethod]
        public void TestTimeLimitConfigurationCaseD()
        {
            TemplateMethod("error", true, 5);
        }

        public void TemplateMethod(string timeLimitsKey, bool exceptionExpected, long expectedTimeLimit)
        {
            string contents = GetConfigContents(TimeLimits[timeLimitsKey]);

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
                var timeLimit = section.TimeLimit;

                Assert.AreEqual(expectedTimeLimit, timeLimit);
            }
        }

        public static string GetConfigContentsFromKeys(string timeLimitsKey)
        {
            return GetConfigContents(TimeLimits[timeLimitsKey]);
        }

        private static string GetConfigContents(string timeLimit)
        {
            return string.Format(Resources.TimeLimitConfig, timeLimit
                , TestCrawler.GetTestUrl("/TimeLimitTests/Sleep.aspx")
            );
        }
    }
}
