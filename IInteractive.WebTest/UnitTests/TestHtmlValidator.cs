using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.MarkupValidator;
using IInteractive.WebConsole;
using IInteractive.WebTest.Properties;
using System.Configuration;
using System.IO;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestHtmlValidator
    {

        public static string ValidatorUri = "http://validator.iinteractive.com/check";

        [TestMethod]
        public void TestHtmlValidatorCaseA()
        {
            TemplateMethod(ValidatorUri, TestCrawler.GetTestUrl("/HtmlValidatorTests/CaseA/Seed.aspx"), 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestHtmlValidatorCaseB()
        {
            TemplateMethod(ValidatorUri, TestCrawler.GetTestUrl("/HtmlValidatorTests/CaseB/Seed.aspx"), 1, 0, 0, 0);
        }

        [TestMethod]
        public void TestHtmlValidatorCaseC()
        {
            TemplateMethod(ValidatorUri, TestCrawler.GetTestUrl("/HtmlValidatorTests/CaseC/Seed.aspx"), 0, 2, 2, 0);
        }

        public void TemplateMethod(string validatorUri, string validateUri, int expectedErrors, int expectedWarnings, int expectedWarningPotentialIssues, int expectedFaults)
        {
            var validator = new HtmlValidator(new Uri(validatorUri));

            var template = Resources.TestHtmlValidatorTemplate;
            var contents = string.Format(template, validateUri);
            var config = TestConfigurationSections.RetrieveConfig(contents);
            var generator = new WebSiteTestSuiteGenerator((LinkCheckerConfigSection)config.Sections["linkCheckerConfig"], true, false);
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
                validator.Validate(result);
                validations.Add(result.HtmlValidationResult);
                Console.WriteLine("Charset = {0}", result.Charset);
            }

            Assert.AreEqual(1, validations.Count);

            Assert.AreEqual(expectedErrors, int.Parse(validations[0].Errors.errorsCount));
            Assert.AreEqual(expectedWarnings, int.Parse(validations[0].Warnings.warningCount));
            Assert.AreEqual(expectedWarningPotentialIssues, validations[0].WarningPotentialIssues.warningPotentialIssueList.Count);
            Assert.AreEqual(expectedFaults, validations[0].Faults.faultList.Count);
        }

        public static string[] MissingW3CEncodings = new string[] {
            "utf-32"
        };

        public static string[] MissingDotNetEncodings = new string[] {
            "iso-8859-6-i"
        };

        public static string[] BadEncodings = new string[] {
            "gdsafjdsfel"
        };

        public static string[] CommonEncodings = new string[] {
            "iso-8859-1"
        };

        public static string AutoDetect = "(detect automatically)";



        [TestMethod]
        public void TestMissingW3CEncodings()
        {
            foreach (var missingW3CEncoding in MissingW3CEncodings)
            {
                TestDetermineCharsetTemplate(true, missingW3CEncoding, missingW3CEncoding);
                TestDetermineCharsetTemplate(false, missingW3CEncoding, missingW3CEncoding);
            }
        }

        [TestMethod]
        public void TestMissingWDotNetEncodings()
        {
            foreach (var missingDotNetEncoding in MissingDotNetEncodings)
            {
                TestDetermineCharsetTemplate(true, missingDotNetEncoding, null, typeof(NonDotNetEncodingException));
                TestDetermineCharsetTemplate(false, missingDotNetEncoding, null, typeof(NonDotNetEncodingException));
            }
        }

        [TestMethod]
        public void TestBadEncodings()
        {
            foreach (var badEncoding in BadEncodings)
            {
                TestDetermineCharsetTemplate(true, badEncoding);
                TestDetermineCharsetTemplate(false, badEncoding);
            }
        }

        [TestMethod]
        public void TestCommonEncodings()
        {
            foreach (var commonEncoding in CommonEncodings)
            {
                TestDetermineCharsetTemplate(true, commonEncoding, commonEncoding);
                TestDetermineCharsetTemplate(false, commonEncoding, commonEncoding);
            }
        }


        public void TestDetermineCharsetTemplate(bool MetaTag, string Charset)
        {
            TestDetermineCharsetTemplate(MetaTag, Charset, null, typeof(NonDotNetEncodingException));
        }

        public void TestDetermineCharsetTemplate(bool MetaTag, string Charset, string expectedCharset)
        {
            TestDetermineCharsetTemplate(MetaTag, Charset, expectedCharset, null);
        }

        public void TestDetermineCharsetTemplate(bool MetaTag, string Charset, string expectedCharset, Type exceptionExpected)
        {
            var charsetUrl = GetCharsetUrl(MetaTag, Charset);
            var validator = new HtmlValidator(new Uri(ValidatorUri));

            var template = Resources.TestHtmlValidatorTemplate;
            var contents = string.Format(template, charsetUrl);
            var config = TestConfigurationSections.RetrieveConfig(contents);
            var generator = new WebSiteTestSuiteGenerator((LinkCheckerConfigSection)config.Sections["linkCheckerConfig"], true, false);
            generator.GenerateTests();

            var results = new List<HttpRequestResult>();
            foreach (var crawler in generator.Crawlers)
            {
                results.AddRange(crawler.HttpRequestResults);
            }

            Assert.AreEqual(1, results.Count);
            foreach (var result in results)
            {
                Assert.AreEqual(Charset, result.Charset);
                try
                {
                    var actualCharset = validator.DetermineCharset(result);
                    Assert.IsTrue(String.IsNullOrEmpty(expectedCharset) && String.IsNullOrEmpty(actualCharset)
                        || !String.IsNullOrEmpty(expectedCharset) && !String.IsNullOrEmpty(actualCharset) 
                            && expectedCharset.Equals(actualCharset, StringComparison.CurrentCultureIgnoreCase)
                    );

                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, exceptionExpected);
                }
            }
        }

        

        
        


        public string GetCharsetUrl(bool MetaTag, string Charset)
        {
            var path = "/W3CCharsetTests/?";
            if (MetaTag)
                path += "MetaTag=true&amp;";
            path += "Charset=" + Charset;

            return TestCrawler.GetTestUrl(path);
        }




    }
}
