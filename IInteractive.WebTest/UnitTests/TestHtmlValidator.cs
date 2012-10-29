using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.MarkupValidator;
using IInteractive.WebConsole;
using IInteractive.WebTest.Properties;
using System.Configuration;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestHtmlValidator
    {
        [TestMethod]
        public void TestHtmlValidatorCaseA()
        {
            TemplateMethod("http://validator.iinteractive.com/check", TestCrawler.GetTestUrl("/HtmlValidatorTests/CaseA/Seed.htm"), 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestHtmlValidatorCaseB()
        {
            TemplateMethod("http://validator.iinteractive.com/check", TestCrawler.GetTestUrl("/HtmlValidatorTests/CaseB/Seed.htm"), 1, 0, 0, 0);
        }

        [TestMethod]
        public void TestHtmlValidatorCaseC()
        {
            TemplateMethod("http://validator.iinteractive.com/check", TestCrawler.GetTestUrl("/HtmlValidatorTests/CaseC/Seed.htm"), 0, 1, 0, 0);
        }

        [TestMethod]
        public void TestHtmlValidatorCaseD()
        {
            TemplateMethod("http://validator.iinteractive.com/check", TestCrawler.GetTestUrl("/HtmlValidatorTests/CaseD/Seed.htm"), 0, 1, 1, 0);
        }

        public void TemplateMethod(string validatorUri, string validateUri, int expectedErrors, int expectedWarnings, int expectedWarningPotentialIssues, int expectedFaults)
        {
            var validator = new HtmlValidator(new Uri(validatorUri));

            var template = Resources.TestHtmlValidatorTemplate;
            var contents = string.Format(template, validateUri);
            var config = TestConfigurationSections.RetrieveConfig(contents);
            var generator = new WebSiteTestSuiteGenerator((LinkCheckerConfigSection)config.Sections["linkCheckerConfig"]);
            generator.GenerateTests();

            var results = new List<HttpRequestResult>();
            foreach (var crawler in generator.Crawlers)
            {
                results.AddRange(crawler.HttpRequestResults);
                results = results.Distinct().ToList();
            }

            var validations = new List<HtmlValidationResult>();
            foreach (var result in results)
            {
                validations.Add(validator.Validate(result));
            }

            Assert.AreEqual(1, validations.Count);

            Assert.AreEqual(expectedErrors, validations[0].Errors.errorsCount);
            Assert.AreEqual(expectedWarnings, validations[0].Warnings.warningCount);
            Assert.AreEqual(expectedWarningPotentialIssues, validations[0].WarningPotentialIssues.warningPotentialIssueList.Count);
            Assert.AreEqual(expectedFaults, validations[0].Faults.faultList.Count);
        }

    }
}
