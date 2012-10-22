using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebConsole;

namespace IInteractive.WebTest.UnitTests
{
    public class CssParserTestCase
    {
        public CssParserTestCase(string Text)
        {
            this.Text = Text;
            this.Expected = new List<CssUrl>();
        }

        public CssParserTestCase(string Text, params CssUrl[] cssUrls)
        {
            this.Text = Text;
            this.Expected = new List<CssUrl>();
            Expected.AddRange(cssUrls);
        }

        public CssParserTestCase(string Text, List<CssUrl> Expected)
        {
            this.Text = Text;
            this.Expected = Expected;
        }

        public string Text;
        public List<CssUrl> Expected;

        public CssParserTestCase Merge(CssParserTestCase testCase)
        {
            var added = new List<CssUrl>();
            added.AddRange(this.Expected);
            added.AddRange(testCase.Expected);
            return new CssParserTestCase(this.Text + testCase.Text, added);
        }

        public void PerformTest()
        {
            var parser = new CssParser(new HttpRequestResult() { ResultUrl = ParserTestParts.ROOT, Content = this.Text });
            List<Link> links = parser.Parse();

            AssertEqual(this.Expected, links);
        }

        public static void AssertExactlyEqual(CssUrl expected, CssUrl actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.IsTrue(expected.AbsoluteUri == null && actual.AbsoluteUri == null
                || expected.AbsoluteUri != null && actual.AbsoluteUri != null);
            Assert.IsTrue(expected.Ex == null && actual.Ex == null
                || expected.Ex != null && actual.Ex != null);

            Assert.IsTrue(expected.Content != null && actual.Content != null);
            Assert.IsTrue(expected.Path != null && actual.Path != null);
            Assert.IsTrue(expected.Root != null && actual.Root != null);

            if (expected.AbsoluteUri != null)
            {
                Assert.AreEqual(expected.AbsoluteUri.ToString(), actual.AbsoluteUri.ToString(), true);
            }

            Assert.AreEqual(expected.Content, actual.Content, true);
            Assert.AreEqual(expected.Path, actual.Path, true);
        }

        private void AssertEqual(List<CssUrl> expected, List<Link> actual)
        {
            foreach (var item in actual)
            {
                Assert.IsTrue(item is CssUrl, string.Format("Failed to convert link with path, {0}, to CssUrl", item.Path ?? "null"));
            }



            Assert.AreEqual(expected.Count, actual.Count, string.Format("text = {0}", this.Text));
            for (int i = 0; i < expected.Count; i++)
            {
                AssertExactlyEqual(expected[i], (CssUrl)actual[i]);
            }
        }

        public static List<CssParserTestCase> GenerateTestCases()
        {
            var testCases = new List<CssParserTestCase>();
            var parts = GenerateUrlTags();
            foreach (var part1 in parts)
            {
                var expected = new List<CssUrl>();
                var cssUrl = new CssUrl(ParserTestParts.ROOT, part1.Path);
                cssUrl.Content = part1.Content;
                expected.Add(cssUrl);
                string comment = string.Format(ParserTestParts.CSS_COMMENT_TEMPLATES[0], part1.Content);
                string content1 = part1.Content;
                string content2 = comment + part1.Content;
                string content3 = part1.Content + comment;
                string content4 = comment + part1.Content + comment;
                testCases.Add(new CssParserTestCase(content1, expected));
                testCases.Add(new CssParserTestCase(content2, expected));
                testCases.Add(new CssParserTestCase(content3, expected));
                testCases.Add(new CssParserTestCase(content4, expected));
            }
            testCases.Add(testCases[3].Merge(testCases[testCases.Count - 4]));
            testCases.AddRange(GenerateBorderTestCases());

            return testCases;
        }

        public static CssParserTestCase[] GenerateBorderTestCases()
        {
            return new CssParserTestCase[] {
                new CssParserTestCase(""),
                new CssParserTestCase("#fdajfklewafka { margin: 2 4 1 2 }")
            };
        }

        public static CssTestCasePart[] GenerateUrlTags()
        {
            int numUris = 5;
            int numStrings = ParserTestParts.WHITE_SPACES_2.Length
                * ParserTestParts.WHITE_SPACES_2.Length
                * ParserTestParts.WHITE_SPACES_2.Length 
                * numUris
                * ParserTestParts.URL_TEMPLATES.Length;
            CssTestCasePart[] tags = new CssTestCasePart[numStrings];

            int ctr = 0;
            foreach (var ws1 in ParserTestParts.WHITE_SPACES_2)
            {
                foreach (var ws2 in ParserTestParts.WHITE_SPACES_2)
                {
                    for (int i = 0; i < numUris; i++)
                    {
                        foreach (var ws3 in ParserTestParts.WHITE_SPACES_2)
                        {
                            foreach (var template in ParserTestParts.URL_TEMPLATES)
                            {
                                tags[ctr] = new CssTestCasePart(string.Format(template, ws1, ws2, ParserTestParts.URIS[i], ws3), ParserTestParts.URIS[i]);
                                ctr++;
                            }
                        }
                    }
                }
            }
            return tags;
        }
    }

    public class CssTestCasePart
    {
        public CssTestCasePart(string Content, string Path)
        {
            this.Content = Content;
            this.Path = Path;
        }

        public string Content;
        public string Path;
    }

    [TestClass]
    public class TestCssParser
    {
        

        [TestMethod]
        public void TestCssUrlParser()
        {
            var testCases = CssParserTestCase.GenerateTestCases();
            foreach (var testCase in testCases)
            {
                testCase.PerformTest();
            }
        }
    }
}
