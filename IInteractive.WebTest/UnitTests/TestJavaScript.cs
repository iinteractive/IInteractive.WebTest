using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebConsole;

namespace IInteractive.WebTest
{
    public class JavaScriptParserTestCase
    {
        public static JavaScript Create(string Path, string Source, string Content)
        {
            var link = new JavaScript(ParserTestParts.ROOT, Path);
            link.Source = Source;
            link.Content = Content;
            return link;
        }

        public static void AssertExactlyEqual(JavaScript expected, JavaScript actual, params string[] messages)
        {
            string message = "";
            foreach (var item in messages)
            {
                message += item;
            }

            Assert.IsNotNull(expected, message);
            Assert.IsNotNull(actual, message);

            Assert.IsTrue(expected.AbsoluteUri == null && actual.AbsoluteUri == null
                || expected.AbsoluteUri != null && actual.AbsoluteUri != null, message);
            Assert.IsTrue(expected.Ex == null && actual.Ex == null
                || expected.Ex != null && actual.Ex != null, message);

            Assert.IsTrue(expected.Source != null && actual.Source != null, message);
            Assert.IsTrue(expected.Content != null && actual.Content != null, message);
            Assert.IsTrue(expected.Path != null && actual.Path != null, message);
            Assert.IsTrue(expected.Root != null && actual.Root != null, message);

            if (expected.AbsoluteUri != null)
            {
                Assert.AreEqual(expected.AbsoluteUri.ToString(), actual.AbsoluteUri.ToString(), true, message);
            }

            Assert.AreEqual(expected.Source, actual.Source, true, message);
            Assert.AreEqual(expected.Content, actual.Content, true, message);
            Assert.AreEqual(expected.Path, actual.Path, true, message);
        }

        public static List<JavaScriptParserTestCase> GenerateBadTestCases()
        {
            int seed = DateTime.Now.Millisecond;
            Random rand = new Random(seed);
            Console.WriteLine("seed = {0}", seed);

            var testCases = new List<JavaScriptParserTestCase>();
            foreach (var item in ParserTestParts.SRC_TEMPLATES)
            {
                string hrefContent = string.Format(item, ParserTestParts.URIS[rand.Next(4)]);
                string wsContent = ParserTestParts.WHITE_SPACES[rand.Next(ParserTestParts.WHITE_SPACES.Length)];
                string textContent = ParserTestParts.TEXT[rand.Next(ParserTestParts.TEXT.Length)];


                for (int i = 1; i < ParserTestParts.SCRIPT_TEMPLATES.Length; i++)
                {
                    string badContent = string.Format(ParserTestParts.SCRIPT_TEMPLATES[i], wsContent + hrefContent, textContent);
                    testCases.Add(new JavaScriptParserTestCase(badContent));
                }
            }

            return testCases;
        }

        public static List<JavaScriptParserTestCase> GenerateGoodTestCases()
        {
            // Initialization
            TestCasePart[] attributeInformation =
                GenerateAttributes();
            string[] leftOfAttr = GoodLeftOfHrefText();
            string[] rightOfAttr = GoodRightOfHrefText();
            string[] text = ParserTestParts.TEXT;

            int numTestCases = attributeInformation.Length * leftOfAttr.Length * rightOfAttr.Length * text.Length;
            Console.WriteLine("Number of test cases = {0}", numTestCases);
            var testCases = new List<JavaScriptParserTestCase>(numTestCases);
            foreach (var part in attributeInformation)
            {
                foreach (var left in leftOfAttr)
                {
                    foreach (var right in rightOfAttr)
                    {
                        foreach (var inner in text)
                        {
                            string content = string.Format(ParserTestParts.SCRIPT_TEMPLATES[0], left + part.Attr + right, inner);
                            JavaScriptParserTestCase testCase = null;
                            testCase = new JavaScriptParserTestCase(content, Create(part.Path, inner, content));

                            testCases.Add(testCase);
                        }
                    }
                }
            }

            return testCases;
        }

        public static string[] GoodLeftOfHrefText()
        {
            // Initialization
            int numStrings = ParserTestParts.WHITE_SPACES.Length
                + ParserTestParts.WHITE_SPACES.Length * ParserTestParts.OTHER_SCRIPT_ATTRIBUTES.Length * ParserTestParts.WHITE_SPACES.Length;
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
                for (int j = 0; j < ParserTestParts.OTHER_SCRIPT_ATTRIBUTES.Length; j++)
                {
                    for (int k = 0; k < ParserTestParts.WHITE_SPACES.Length; k++)
                    {
                        strings[counter] = ParserTestParts.WHITE_SPACES[i] + ParserTestParts.OTHER_SCRIPT_ATTRIBUTES[j] + ParserTestParts.WHITE_SPACES[k];
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
            foreach (var item in ParserTestParts.SRC_TEMPLATES)
            {
                doublesSrcs += item.Contains("\"") ? 1 : 0;
                singlesSrcs += item.Contains("'") ? 1 : 0;
            }

            // Initializations
            int numAttributes = ParserTestParts.SRC_TEMPLATES.Length * ParserTestParts.URIS.Length - doublesUris * doublesSrcs 
                - singlesUris * singlesSrcs + ParserTestParts.SRC_TEMPLATES.Length * ParserTestParts.FORMAT_EXCEPTION_URIS.Length;
            TestCasePart[] parts = new TestCasePart[numAttributes];

            // Generation
            int counter = 0;
            for (int i = 0; i < ParserTestParts.SRC_TEMPLATES.Length; i++)
            {
                // Good Uris
                for (int j = 0; j < ParserTestParts.URIS.Length; j++)
                {
                    if(!(ParserTestParts.URIS[j].Contains("\"") && ParserTestParts.SRC_TEMPLATES[i].Contains("\"")
                        || ParserTestParts.URIS[j].Contains("'") && ParserTestParts.SRC_TEMPLATES[i].Contains("'")))
                    {
                        parts[counter] = new TestCasePart(
                        string.Format(ParserTestParts.SRC_TEMPLATES[i], ParserTestParts.URIS[j])
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
                        string.Format(ParserTestParts.SRC_TEMPLATES[i], ParserTestParts.FORMAT_EXCEPTION_URIS[j])
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
        public List<JavaScript> ExpectedLinks;

        public JavaScriptParserTestCase(string ContentToParse, List<JavaScript> ExpectedLinks)
        {
            this.ContentToParse = ContentToParse;
            this.ExpectedLinks = ExpectedLinks;
        }

        public JavaScriptParserTestCase(string ContentToParse, params JavaScript[] ExpectedLinks)
        {
            this.ContentToParse = ContentToParse;
            this.ExpectedLinks = new List<JavaScript>();
            this.ExpectedLinks.AddRange(ExpectedLinks);
        }

        public JavaScriptParserTestCase Merge(JavaScriptParserTestCase arg)
        {
            var newContent = this.ContentToParse + arg.ContentToParse;
            var newLinks = new List<JavaScript>();
            newLinks.AddRange(this.ExpectedLinks);
            newLinks.AddRange(arg.ExpectedLinks);

            return new JavaScriptParserTestCase(newContent, newLinks);
        }

        public void PerformTest()
        {
            var parser = new HtmlParser(new HttpRequestResult() { ResultUrl = ParserTestParts.ROOT, Content = this.ContentToParse });
            List<Link> links = parser.Parse();

            AssertEqual(this.ExpectedLinks, links);
        }

        private void AssertEqual(List<JavaScript> expected, List<Link> actual)
        {
            foreach (var item in actual)
            {
                Assert.IsTrue(item is JavaScript, string.Format("Failed to convert link with path, {0}, to JavaScript", item.Path ?? "null"));
            }



            Assert.AreEqual(expected.Count, actual.Count, string.Format("content = {0}", this.ContentToParse));
            for (int i = 0; i < expected.Count; i++)
            {
                AssertExactlyEqual(expected[i], (JavaScript)actual[i], string.Format("content = {0}", this.ContentToParse));
            }
        }

    }

    [TestClass]
    public class TestJavaScript
    {
        [TestMethod]
        public void TestJavaScriptParse()
        {
            var tests = JavaScriptParserTestCase.GenerateGoodTestCases();
            var badTests = JavaScriptParserTestCase.GenerateBadTestCases();
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
