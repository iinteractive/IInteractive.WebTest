using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebConsole;

namespace IInteractive.WebTest.UnitTests
{
    public class StyleSheetParserTestCase
    {
        public static StyleSheet Create(string Path, string Content)
        {
            var link = new StyleSheet(ParserTestParts.ROOT, Path);
            link.Content = Content;
            return link;
        }

        public static void AssertExactlyEqual(StyleSheet expected, StyleSheet actual)
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

        public static List<StyleSheetParserTestCase> GenerateBadTestCases()
        {
            int seed = DateTime.Now.Millisecond;
            Random rand = new Random(seed);
            Console.WriteLine("seed = {0}", seed);

            var testCases = new List<StyleSheetParserTestCase>();
            foreach (var item in ParserTestParts.HREF_TEMPLATES)
            {
                string hrefContent = string.Format(item, ParserTestParts.URIS[rand.Next(4)]);
                string wsContent = ParserTestParts.WHITE_SPACES[rand.Next(ParserTestParts.WHITE_SPACES.Length)];

                for (int i = 2; i < ParserTestParts.STYLESHEET_TEMPLATES.Length; i++)
                {
                    string badContent = string.Format(ParserTestParts.STYLESHEET_TEMPLATES[i], wsContent + hrefContent);
                    testCases.Add(new StyleSheetParserTestCase(badContent));
                }
            }

            return testCases;
        }

        public static List<StyleSheetParserTestCase> GenerateGoodTestCases()
        {
            // Initialization
            TestCasePart[] attributeInformation =
                GenerateAttributes();
            LeftRightAttributes[] otherAttrs = GoodLeftRightAttributes();

            int numTestCases = attributeInformation.Length * otherAttrs.Length * 2;
            Console.WriteLine("Number of test cases = {0}", numTestCases);
            var testCases = new List<StyleSheetParserTestCase>(numTestCases);
            foreach (var part in attributeInformation)
            {
                foreach (var otherAttr in otherAttrs)
                {
                    string content = string.Format(ParserTestParts.STYLESHEET_TEMPLATES[0], otherAttr.Left + part.Attr + otherAttr.Right);
                        StyleSheetParserTestCase testCase = null;
                        testCase = new StyleSheetParserTestCase(content, Create(part.Path, content));

                        testCases.Add(testCase);

                        content = string.Format(ParserTestParts.STYLESHEET_TEMPLATES[1], otherAttr.Left + part.Attr + otherAttr.Right);
                        testCase = new StyleSheetParserTestCase(content, Create(part.Path, content));

                        testCases.Add(testCase);
                }
            }

            return testCases;
        }

        public static LeftRightAttributes[] GoodLeftRightAttributes()
        {
            int numGoods = ParserTestParts.WHITE_SPACES.Length * ParserTestParts.WHITE_SPACES.Length * ParserTestParts.OTHER_STYLESHEET_ATTRIBUTES.Length * ParserTestParts.WHITE_SPACES.Length * 2;

            var attrs = new LeftRightAttributes[numGoods];
            int ctr = 0;
            for (int i = 0; i < 2; i++)
            {
                foreach (var ws0 in ParserTestParts.WHITE_SPACES)
                {
                    foreach (var ws1 in ParserTestParts.WHITE_SPACES)
                    {
                        foreach (var styleAttr in ParserTestParts.OTHER_STYLESHEET_ATTRIBUTES)
                        {
                            foreach (var ws2 in ParserTestParts.WHITE_SPACES)
                            {
                                string content1 = ws1 + styleAttr + ws2;
                                string content2 = ws0;
                                attrs[ctr] = i == 0 ? new LeftRightAttributes(content1, content2) : new LeftRightAttributes(content2, content1);
                                ctr++;
                            }
                        }
                    }
                }
            }

            return attrs;

        }

        public static string[] GoodLeftOfHrefText()
        {
            // Initialization
            int numStrings = ParserTestParts.WHITE_SPACES.Length
                + ParserTestParts.WHITE_SPACES.Length * ParserTestParts.OTHER_STYLESHEET_ATTRIBUTES.Length * ParserTestParts.WHITE_SPACES.Length;
            string[] strings = new string[numStrings];

            // Generation
            int counter = 0;
            for (int i = 0; i < ParserTestParts.WHITE_SPACES.Length; i++)
            {
                strings[counter] = ParserTestParts.WHITE_SPACES[i];
                counter++;
            }

            for (int i = 0; i < ParserTestParts.WHITE_SPACES.Length; i++)
            {
                for (int j = 0; j < ParserTestParts.OTHER_STYLESHEET_ATTRIBUTES.Length; j++)
                {
                    for (int k = 0; k < ParserTestParts.WHITE_SPACES.Length; k++)
                    {
                        strings[counter] = ParserTestParts.WHITE_SPACES[i] + ParserTestParts.OTHER_STYLESHEET_ATTRIBUTES[j] + ParserTestParts.WHITE_SPACES[k];
                        counter++;
                    }
                }
            }

            return strings;
        }

        public static string[] GoodRightOfHrefText()
        {
            string[] leftStrings = GoodLeftOfHrefText();

            // Initialization
            int numStrings = leftStrings.Length + 1;
            string[] strings = new string[numStrings];

            // Generation
            int counter = 0;
            for (int i = 0; i < leftStrings.Length; i++)
            {
                strings[counter] = leftStrings[i];
                counter++;
            }

            strings[counter] = "";
            counter++;

            return strings;
        }

        public static TestCasePart[] GenerateAttributes()
        {
            int doublesUris = 0;
            int singlesUris = 0;
            int doublesSrcs = 0;
            int singlesSrcs = 0;
            foreach (var item in ParserTestParts.URIS)
            {
                doublesUris += item.Contains("\"") ? 1 : 0;
                singlesUris += item.Contains("'") ? 1 : 0;
            }
            foreach (var item in ParserTestParts.HREF_TEMPLATES)
            {
                doublesSrcs += item.Contains("\"") ? 1 : 0;
                singlesSrcs += item.Contains("'") ? 1 : 0;
            }

            // Initializations
            int numAttributes = ParserTestParts.HREF_TEMPLATES.Length * ParserTestParts.URIS.Length - doublesUris * doublesSrcs
                - singlesUris * singlesSrcs + ParserTestParts.HREF_TEMPLATES.Length * ParserTestParts.FORMAT_EXCEPTION_URIS.Length;
            TestCasePart[] parts = new TestCasePart[numAttributes];

            // Generation
            int counter = 0;
            for (int i = 0; i < ParserTestParts.HREF_TEMPLATES.Length; i++)
            {
                // Good Uris
                for (int j = 0; j < ParserTestParts.URIS.Length; j++)
                {
                    if (!(ParserTestParts.URIS[j].Contains("\"") && ParserTestParts.HREF_TEMPLATES[i].Contains("\"")
                        || ParserTestParts.URIS[j].Contains("'") && ParserTestParts.HREF_TEMPLATES[i].Contains("'")))
                    {
                        parts[counter] = new TestCasePart(
                            string.Format(ParserTestParts.HREF_TEMPLATES[i], ParserTestParts.URIS[j])
                            , ParserTestParts.URIS[j]
                            , IsCorrectScheme(ParserTestParts.URIS[j])
                        );
                        counter++;
                    }
                }

                // Bad Uris
                for (int j = 0; j < ParserTestParts.FORMAT_EXCEPTION_URIS.Length; j++)
                {
                    parts[counter] = new TestCasePart(
                        string.Format(ParserTestParts.HREF_TEMPLATES[i], ParserTestParts.FORMAT_EXCEPTION_URIS[j])
                        , ParserTestParts.FORMAT_EXCEPTION_URIS[j]
                        , IsCorrectScheme(ParserTestParts.FORMAT_EXCEPTION_URIS[j])
                    );
                    counter++;
                }
            }

            return parts;
        }

        private static bool IsCorrectScheme(string path)
        {
            if (path.StartsWith("mailto", StringComparison.CurrentCultureIgnoreCase))
                return false;
            else if (path.StartsWith("tel", StringComparison.CurrentCultureIgnoreCase))
                return false;
            else if (path.StartsWith("javascript", StringComparison.CurrentCultureIgnoreCase))
                return false;
            else
                return true;
        }

        public string ContentToParse;
        public List<StyleSheet> ExpectedLinks;

        public StyleSheetParserTestCase(string ContentToParse, List<StyleSheet> ExpectedLinks)
        {
            this.ContentToParse = ContentToParse;
            this.ExpectedLinks = ExpectedLinks;
        }

        public StyleSheetParserTestCase(string ContentToParse, params StyleSheet[] ExpectedLinks)
        {
            this.ContentToParse = ContentToParse;
            this.ExpectedLinks = new List<StyleSheet>();
            this.ExpectedLinks.AddRange(ExpectedLinks);
        }

        public StyleSheetParserTestCase Merge(StyleSheetParserTestCase arg)
        {
            var newContent = this.ContentToParse + arg.ContentToParse;
            var newLinks = new List<StyleSheet>();
            newLinks.AddRange(this.ExpectedLinks);
            newLinks.AddRange(arg.ExpectedLinks);

            return new StyleSheetParserTestCase(newContent, newLinks);
        }

        public void PerformTest()
        {
            var parser = new HtmlParser(new HttpRequestResult() { ResultUrl = ParserTestParts.ROOT, Content = this.ContentToParse });
            List<Link> links = parser.Parse();

            AssertEqual(this.ExpectedLinks, links);
        }

        private void AssertEqual(List<StyleSheet> expected, List<Link> actual)
        {
            foreach (var item in actual)
            {
                Assert.IsTrue(item is StyleSheet, string.Format("Failed to convert link with path, {0}, to StyleSheet", item.Path ?? "null"));
            }



            Assert.AreEqual(expected.Count, actual.Count, string.Format("content = {0}", this.ContentToParse));
            for (int i = 0; i < expected.Count; i++)
            {
                AssertExactlyEqual(expected[i], (StyleSheet)actual[i]);
            }
        }

    }

    public class LeftRightAttributes
    {
        public LeftRightAttributes(string Left, string Right)
        {
            this.Left = Left;
            this.Right = Right;
        }

        public string Left;
        public string Right;
    }

    [TestClass]
    public class TestStyleSheet
    {
        [TestMethod]
        public void TestStyleSheetParse()
        {
            var tests = StyleSheetParserTestCase.GenerateGoodTestCases();
            var badTests = StyleSheetParserTestCase.GenerateBadTestCases();
            tests.AddRange(badTests);
            int ctr = 0;
            for (int i = 0; i < tests.Count; i++)
            {
                var item = tests[i];
                item.PerformTest();

                if (item.ExpectedLinks.Count == 1 && item.ExpectedLinks[0].AbsoluteUri != null && ctr < 3)
                {
                    foreach (var badItem in badTests)
                    {
                        tests.Add(badItem.Merge(item));
                        tests.Add(item.Merge(badItem));
                    }
                    ctr++;
                }
            }
        }
    }
}
