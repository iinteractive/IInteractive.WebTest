using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using IInteractive.WebConsole;
using IInteractive.WebTest.Properties;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestProgram
    {

        [TestMethod]
        public void TestBadArgs()
        {
            var argsList = new List<string[]>();
            var configurationFilesSource = new TestConfigurationSections();
            argsList.Add(new string[] {});
            argsList.Add(new string[] {"fdsafdsa"});
            argsList.Add(new string[] { "-c:" + SaveToTempFile(TestConfigurationSections.GetNormalConfig()), "fdsafdsa" });
            TestArgsVsReturnCode(argsList, 1);
        }

        [TestMethod]
        public void TestBadReturnCodes()
        {
            List<string> badConfigFiles = new List<string>();
            badConfigFiles.Add(SaveToTempFile(TestConfigurationSections.GetNoConfigSections()));
            badConfigFiles.Add(SaveToTempFile(TestConfigurationSections.GetNoSeedsConfig()));
            badConfigFiles.Add("DOESNOTEXIST");
            badConfigFiles.Add("/////////////");
            badConfigFiles.Add(SaveToTempFile(string.Format(Resources.ForbiddenConfig, TestCrawler.LocalHost, TestCrawler.GetTestUrl("/"))));
            badConfigFiles.Add(SaveToTempFile(TestBrokenLinksConfigurations.GetConfigContentsFromKeys("uri", "error")));
            badConfigFiles.Add(SaveToTempFile(TestBrokenLinksConfigurations.GetConfigContentsFromKeys("path", "true")));
            badConfigFiles.Add(SaveToTempFile(TestTimeLimitConfigurations.GetConfigContentsFromKeys("error")));

            TestFilesVsReturnCode(badConfigFiles, 1);
        }

        [TestMethod]
        public void TestGoodReturnCodes()
        {
            List<string> goodConfigFiles = new List<string>();
            goodConfigFiles.Add(SaveToTempFile(TestConfigurationSections.GetNoBrowsersConfig()));
            goodConfigFiles.Add(SaveToTempFile(TestConfigurationSections.GetNormalConfig()));
            goodConfigFiles.Add(SaveToTempFile(TestConfigurationSections.GetNoLinkCheckerAttrConfig()));
            goodConfigFiles.Add(SaveToTempFile(TestBrokenLinksConfigurations.GetConfigContentsFromKeys("uri", "true")));
            goodConfigFiles.Add(SaveToTempFile(TestTimeLimitConfigurations.GetConfigContentsFromKeys("pass")));

            TestFilesVsReturnCode(goodConfigFiles, 0);
        }

        private void TestFilesVsReturnCode(List<string> configFiles, int expectedReturnCode)
        {
            var argsList = new List<string[]>();

            foreach (var fileName in configFiles)
            {
                string[] args = new string[] {"-c:" + fileName};
                argsList.Add(args);
                
            }
            TestArgsVsReturnCode(argsList, expectedReturnCode);
        }

        private void TestArgsVsReturnCode(List<string[]> argsList, int expectedReturnCode)
        {
            foreach (var args in argsList)
            {
                var program = new Program(args);
                Assert.AreEqual(expectedReturnCode, program.Execute());
            }
        }

        private string SaveToTempFile(string contents)
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
            using (StreamReader reader = new StreamReader(fileName))
            {
                string fileContents = reader.ReadToEnd();
                Console.WriteLine("FILE CONTENTS-------------------------\n{0}\n/FILE CONTENTS------------------------", fileContents);
            }

            return fileName;
        }
    }
}
