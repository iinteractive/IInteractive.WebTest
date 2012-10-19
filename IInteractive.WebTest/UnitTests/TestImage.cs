using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebConsole;

namespace IInteractive.WebTest.UnitTests
{
    public class ImageParserTestCase
    {
        public static Image Create(string Path, string Content)
        {
            var link = new Image(ParserTestParts.ROOT, Path);
            link.Content = Content;
            return link;
        }

        public static void AssertExactlyEqual(Image expected, Image actual)
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

        public static List<ImageParserTestCase> GenerateBadTestCases()
        {
            int seed = DateTime.Now.Millisecond;
            Random rand = new Random(seed);
            Console.WriteLine("seed = {0}", seed);

            var testCases = new List<ImageParserTestCase>();
            foreach (var item in ParserTestParts.SRC_TEMPLATES)
            {
                string hrefContent = string.Format(item, ParserTestParts.URIS[rand.Next(4)]);
                string wsContent = ParserTestParts.WHITE_SPACES[rand.Next(ParserTestParts.WHITE_SPACES.Length)];

                for (int i = 2; i < ParserTestParts.IMAGE_TEMPLATES.Length; i++)
                {
                    string badContent = string.Format(ParserTestParts.IMAGE_TEMPLATES[i], wsContent + hrefContent);
                    testCases.Add(new ImageParserTestCase(badContent));
                }
            }

            return testCases;
        }

        public static List<ImageParserTestCase> GenerateGoodTestCases()
        {
            // Initialization
            TestCasePart[] attributeInformation =
                GenerateAttributes();
            string[] leftOfAttr = GoodLeftOfHrefText();
            string[] rightOfAttr = GoodRightOfHrefText();

            int numTestCases = attributeInformation.Length * leftOfAttr.Length * rightOfAttr.Length * 2;
            Console.WriteLine("Number of test cases = {0}", numTestCases);
            var testCases = new List<ImageParserTestCase>(numTestCases);
            foreach (var part in attributeInformation)
            {
                foreach (var left in leftOfAttr)
                {
                    foreach (var right in rightOfAttr)
                    {
                        string content = string.Format(ParserTestParts.IMAGE_TEMPLATES[0], left + part.Attr + right);
                        ImageParserTestCase testCase = null;
                        testCase = new ImageParserTestCase(content, Create(part.Path, content));

                        testCases.Add(testCase);

                        content = string.Format(ParserTestParts.IMAGE_TEMPLATES[1], left + part.Attr + right);
                        testCase = new ImageParserTestCase(content, Create(part.Path, content));

                        testCases.Add(testCase);
                    }
                }
            }

            return testCases;
        }

        public static string[] GoodLeftOfHrefText()
        {
            // Initialization
            int numStrings = ParserTestParts.WHITE_SPACES.Length
                + ParserTestParts.WHITE_SPACES.Length * ParserTestParts.OTHER_ATTRIBUTES.Length * ParserTestParts.WHITE_SPACES.Length;
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
                for (int j = 0; j < ParserTestParts.OTHER_ATTRIBUTES.Length; j++)
                {
                    for (int k = 0; k < ParserTestParts.WHITE_SPACES.Length; k++)
                    {
                        strings[counter] = ParserTestParts.WHITE_SPACES[i] + ParserTestParts.OTHER_ATTRIBUTES[j] + ParserTestParts.WHITE_SPACES[k];
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
                    if (!(ParserTestParts.URIS[j].Contains("\"") && ParserTestParts.SRC_TEMPLATES[i].Contains("\"")
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
        public List<Image> ExpectedLinks;

        public ImageParserTestCase(string ContentToParse, List<Image> ExpectedLinks)
        {
            this.ContentToParse = ContentToParse;
            this.ExpectedLinks = ExpectedLinks;
        }

        public ImageParserTestCase(string ContentToParse, params Image[] ExpectedLinks)
        {
            this.ContentToParse = ContentToParse;
            this.ExpectedLinks = new List<Image>();
            this.ExpectedLinks.AddRange(ExpectedLinks);
        }

        public ImageParserTestCase Merge(ImageParserTestCase arg)
        {
            var newContent = this.ContentToParse + arg.ContentToParse;
            var newLinks = new List<Image>();
            newLinks.AddRange(this.ExpectedLinks);
            newLinks.AddRange(arg.ExpectedLinks);

            return new ImageParserTestCase(newContent, newLinks);
        }

        public void PerformTest()
        {
            var parser = new HtmlParser(new HttpRequestResult() { ResultUrl = ParserTestParts.ROOT, Content = this.ContentToParse });
            List<Link> links = parser.Parse();

            AssertEqual(this.ExpectedLinks, links);
        }

        private void AssertEqual(List<Image> expected, List<Link> actual)
        {
            foreach (var item in actual)
            {
                Assert.IsTrue(item is Image, string.Format("Failed to convert link with path, {0}, to Image", item.Path ?? "null"));
            }



            Assert.AreEqual(expected.Count, actual.Count, string.Format("content = {0}", this.ContentToParse));
            for (int i = 0; i < expected.Count; i++)
            {
                AssertExactlyEqual(expected[i], (Image)actual[i]);
            }
        }

    }

    [TestClass]
    public class TestImage
    {
        [TestMethod]
        public void TestImageParse()
        {
            var tests = ImageParserTestCase.GenerateGoodTestCases();
            var badTests = ImageParserTestCase.GenerateBadTestCases();
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
