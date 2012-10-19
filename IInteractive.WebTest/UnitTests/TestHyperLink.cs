using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebConsole;

namespace IInteractive.WebTest
{
    public class HyperLinkParserTestCase
    {
        public static HyperLink Create(string Path, string Text, string Content)
        {
            var link = new HyperLink(ParserTestParts.ROOT, Path);
            link.Text = Text;
            link.Content = Content;
            return link;
        }

        public static void AssertExactlyEqual(HyperLink expected, HyperLink actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.IsTrue(expected.AbsoluteUri == null && actual.AbsoluteUri == null 
                || expected.AbsoluteUri != null && actual.AbsoluteUri != null);
            Assert.IsTrue(expected.Ex == null && actual.Ex == null 
                || expected.Ex != null && actual.Ex != null);

            Assert.IsTrue(expected.Text != null && actual.Text != null);
            Assert.IsTrue(expected.Content != null && actual.Content != null);
            Assert.IsTrue(expected.Path != null && actual.Path != null);
            Assert.IsTrue(expected.Root != null && actual.Root != null);

            if (expected.AbsoluteUri != null)
            {
                Assert.AreEqual(expected.AbsoluteUri.ToString(), actual.AbsoluteUri.ToString(), true);
            }

            Assert.AreEqual(expected.Text, actual.Text, true);
            Assert.AreEqual(expected.Content, actual.Content, true);
            Assert.AreEqual(expected.Path, actual.Path, true);
        }

        public static List<HyperLinkParserTestCase> GenerateBadTestCases()
        {
            int seed = DateTime.Now.Millisecond;
            Random rand = new Random(seed);
            Console.WriteLine("seed = {0}", seed);

            var testCases = new List<HyperLinkParserTestCase>();
            foreach (var item in ParserTestParts.HREF_TEMPLATES)
            {
                string hrefContent = string.Format(item, ParserTestParts.URIS[rand.Next(4)]);
                string wsContent = ParserTestParts.WHITE_SPACES[rand.Next(ParserTestParts.WHITE_SPACES.Length)];
                string textContent = ParserTestParts.TEXT[rand.Next(ParserTestParts.TEXT.Length)];


                for (int i = 1; i < ParserTestParts.A_TEMPLATES.Length; i++)
                {
                    string badContent = string.Format(ParserTestParts.A_TEMPLATES[i], wsContent + hrefContent, textContent);
                    testCases.Add(new HyperLinkParserTestCase(badContent));
                }
            }

            return testCases;
        }

        public static List<HyperLinkParserTestCase> GenerateGoodTestCases()
        {
            // Initialization
            TestCasePart[] attributeInformation = 
                GenerateAttributes();
            string[] leftOfAttr = GoodLeftOfHrefText();
            string[] rightOfAttr = GoodRightOfHrefText();
            string[] text = ParserTestParts.TEXT;

            int numTestCases = attributeInformation.Length * leftOfAttr.Length * rightOfAttr.Length * text.Length;
            Console.WriteLine("Number of test cases = {0}", numTestCases);
            var testCases = new List<HyperLinkParserTestCase>(numTestCases);
            foreach(var part in attributeInformation) {
                foreach(var left in leftOfAttr) {
                    foreach(var right in rightOfAttr) {
                        foreach(var inner in text) {
                            string content = string.Format(ParserTestParts.A_TEMPLATES[0], left + part.Attr + right, inner);
                            HyperLinkParserTestCase testCase = null;
                            if (part.IsCorrectScheme)
                            {
                                testCase = new HyperLinkParserTestCase(content, Create(part.Path, inner, content));
                            }
                            else
                            {
                                testCase = new HyperLinkParserTestCase(content);
                            }
                             
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
                    if (!(ParserTestParts.URIS[j].Contains("\"") && ParserTestParts.SRC_TEMPLATES[i].Contains("\"")
                        || ParserTestParts.URIS[j].Contains("'") && ParserTestParts.SRC_TEMPLATES[i].Contains("'")))
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
            if(path.StartsWith("mailto", StringComparison.CurrentCultureIgnoreCase))
                return false;
            else if(path.StartsWith("tel", StringComparison.CurrentCultureIgnoreCase))
                return false;
            else if(path.StartsWith("javascript", StringComparison.CurrentCultureIgnoreCase))
                return false;
            else
                return true;
        }

        public string ContentToParse;
        public List<HyperLink> ExpectedLinks;

        public HyperLinkParserTestCase(string ContentToParse, List<HyperLink> ExpectedLinks)
        {
            this.ContentToParse = ContentToParse;
            this.ExpectedLinks = ExpectedLinks;
        }

        public HyperLinkParserTestCase(string ContentToParse, params HyperLink[] ExpectedLinks)
        {
            this.ContentToParse = ContentToParse;
            this.ExpectedLinks = new List<HyperLink>();
            this.ExpectedLinks.AddRange(ExpectedLinks);
        }

        public HyperLinkParserTestCase Merge(HyperLinkParserTestCase arg)
        {
            var newContent = this.ContentToParse + arg.ContentToParse;
            var newLinks = new List<HyperLink>();
            newLinks.AddRange(this.ExpectedLinks);
            newLinks.AddRange(arg.ExpectedLinks);

            return new HyperLinkParserTestCase(newContent, newLinks);
        }

        public void PerformTest()
        {
            var parser = new HtmlParser(new HttpRequestResult() { ResultUrl = ParserTestParts.ROOT, Content = this.ContentToParse });
            List<Link> links = parser.Parse();

            AssertEqual(this.ExpectedLinks, links);
        }

        private void AssertEqual(List<HyperLink> expected, List<Link> actual)
        {
            foreach (var item in actual)
            {
                Assert.IsTrue(item is HyperLink, string.Format("Failed to convert link with path, {0}, to HyperLink", item.Path ?? "null"));
            }

            

            Assert.AreEqual(expected.Count, actual.Count, string.Format("content = {0}", this.ContentToParse));
            for (int i = 0; i < expected.Count; i++)
            {
                AssertExactlyEqual(expected[i], (HyperLink)actual[i]);
            }
        }

    }

    public class TestCasePart
    {
        public TestCasePart(string Attr, string Path, bool IsCorrectScheme)
        {
            this.Attr = Attr;
            this.Path = Path;
            this.IsCorrectScheme = IsCorrectScheme;
        }

        public string Attr;
        public string Path;
        public bool IsCorrectScheme;
    }

    [TestClass]
    public class TestHyperLink
    {
        public static string HYPERLINK_TEMPLATE = "<a{0}href=\"{1}\"{2}>{3}</a>";

        [TestMethod]
        public void TestParse()
        {
            var tests = HyperLinkParserTestCase.GenerateGoodTestCases();
            var badTests = HyperLinkParserTestCase.GenerateBadTestCases();
            tests.AddRange(badTests);
            int ctr = 0;
            for(int i = 0; i < tests.Count; i++)
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

        [TestMethod]
        public void TestUriSchemes()
        {
            string http = "http://www.google.com/";
            string https = "https://www.google.com/";
            string mailto = "mailto:someaddress@someaddress.com";
            string js = "javascript:var i = 0;";
            string tel = "tel:+1-201-555-0111";

            Uri httpUri = new Uri(http);
            Uri mailtoUri = new Uri(mailto);
            Uri javascriptUri = new Uri(js);
            Uri telUri = new Uri(tel);
            Uri httpsUri = new Uri(https);

            Assert.AreNotEqual(httpUri.Scheme, mailtoUri.Scheme);
            Assert.AreNotEqual(httpUri.Scheme, javascriptUri.Scheme);
            Assert.AreNotEqual(httpUri.Scheme, telUri.Scheme);
            Assert.AreNotEqual(httpUri.Scheme, httpsUri.Scheme);
            Assert.AreNotEqual(mailtoUri.Scheme, javascriptUri.Scheme);
            Assert.AreNotEqual(mailtoUri.Scheme, telUri.Scheme);
            Assert.AreNotEqual(mailtoUri.Scheme, httpsUri.Scheme);
            Assert.AreNotEqual(javascriptUri.Scheme, telUri.Scheme);
            Assert.AreNotEqual(javascriptUri.Scheme, httpsUri.Scheme);
            Assert.AreNotEqual(telUri.Scheme, httpsUri.Scheme);

            Uri httpBaseMailtoUri = new Uri(httpUri, mailto);
            Uri httpBaseJavascriptUri = new Uri(httpUri, js);
            Uri httpBaseTelUri = new Uri(httpUri, tel);
            Uri httpBaseHttpsUri = new Uri(httpUri, https);

            Assert.AreNotEqual(httpUri.Scheme, httpBaseMailtoUri.Scheme);
            Assert.AreNotEqual(httpUri.Scheme, httpBaseJavascriptUri.Scheme);
            Assert.AreNotEqual(httpBaseMailtoUri.Scheme, httpBaseJavascriptUri.Scheme);
            Assert.AreNotEqual(httpUri.Scheme, httpsUri.Scheme);

            Assert.AreEqual("http", httpUri.Scheme);
            Assert.AreNotEqual("mailto", httpUri.Scheme);
            Assert.AreNotEqual("javascript", httpUri.Scheme);
            Assert.AreNotEqual("tel", httpUri.Scheme);
            Assert.AreNotEqual("https", httpUri.Scheme);

            Assert.AreEqual("mailto", mailtoUri.Scheme);
            Assert.AreNotEqual("http", mailtoUri.Scheme);
            Assert.AreNotEqual("javascript", mailtoUri.Scheme);
            Assert.AreNotEqual("tel", mailtoUri.Scheme);
            Assert.AreNotEqual("https", mailtoUri.Scheme);

            Assert.AreEqual("javascript", javascriptUri.Scheme);
            Assert.AreNotEqual("mailto", javascriptUri.Scheme);
            Assert.AreNotEqual("http", javascriptUri.Scheme);
            Assert.AreNotEqual("tel", javascriptUri.Scheme);
            Assert.AreNotEqual("https", javascriptUri.Scheme);

            Assert.AreEqual("tel", telUri.Scheme);
            Assert.AreNotEqual("mailto", telUri.Scheme);
            Assert.AreNotEqual("http", telUri.Scheme);
            Assert.AreNotEqual("javascript", telUri.Scheme);
            Assert.AreNotEqual("https", telUri.Scheme);

            Assert.AreEqual("https", httpsUri.Scheme);
            Assert.AreNotEqual("mailto", httpsUri.Scheme);
            Assert.AreNotEqual("http", httpsUri.Scheme);
            Assert.AreNotEqual("javascript", httpsUri.Scheme);
            Assert.AreNotEqual("tel", httpsUri.Scheme);

            Assert.AreEqual("mailto", httpBaseMailtoUri.Scheme);
            Assert.AreNotEqual("http", httpBaseMailtoUri.Scheme);
            Assert.AreNotEqual("javascript", httpBaseMailtoUri.Scheme);
            Assert.AreNotEqual("tel", httpBaseMailtoUri.Scheme);
            Assert.AreNotEqual("https", httpBaseMailtoUri.Scheme);

            Assert.AreEqual("javascript", httpBaseJavascriptUri.Scheme);
            Assert.AreNotEqual("mailto", httpBaseJavascriptUri.Scheme);
            Assert.AreNotEqual("http", httpBaseJavascriptUri.Scheme);
            Assert.AreNotEqual("tel", httpBaseJavascriptUri.Scheme);
            Assert.AreNotEqual("https", httpBaseJavascriptUri.Scheme);

            Assert.AreEqual("tel", httpBaseTelUri.Scheme);
            Assert.AreNotEqual("mailto", httpBaseTelUri.Scheme);
            Assert.AreNotEqual("http", httpBaseTelUri.Scheme);
            Assert.AreNotEqual("javascript", httpBaseTelUri.Scheme);
            Assert.AreNotEqual("https", httpBaseTelUri.Scheme);

            Assert.AreEqual("https", httpBaseHttpsUri.Scheme);
            Assert.AreNotEqual("mailto", httpBaseHttpsUri.Scheme);
            Assert.AreNotEqual("http", httpBaseHttpsUri.Scheme);
            Assert.AreNotEqual("javascript", httpBaseHttpsUri.Scheme);
            Assert.AreNotEqual("tel", httpBaseHttpsUri.Scheme);

            Console.WriteLine("httpUri.Scheme = {0}", httpUri.Scheme);
            Console.WriteLine("mailtoUri.Scheme = {0}", mailtoUri.Scheme);
            Console.WriteLine("javascriptUri.Scheme = {0}", javascriptUri.Scheme);
            Console.WriteLine("telUri.Scheme = {0}", telUri.Scheme);
            Console.WriteLine("httpBaseHttpsUri.Scheme = {0}", httpBaseHttpsUri.Scheme);

            Console.WriteLine("httpBaseMailtoUri.Scheme = {0}", httpBaseMailtoUri.Scheme);
            Console.WriteLine("httpBaseJavascriptUri.Scheme = {0}", httpBaseJavascriptUri.Scheme);
            Console.WriteLine("httpBaseTelUri.Scheme = {0}", httpBaseTelUri.Scheme);
            Console.WriteLine("httpBaseHttpsUri.Scheme = {0}", httpBaseHttpsUri.Scheme);
        }

        [TestMethod]
        public void TestCommentParsing()
        {
            NormalGenerateTestTemplate("<!--<a href=\"index.jsp\"></a>--><a href=\"index2.jsp\"></a>", 1, new List<string>() { "http://webcrawlertest2/index2.jsp" });
        }

        [TestMethod]
        public void TestGenerateCaseA()
        {
            NormalGenerateTestTemplate(" ", "index.jsp", "", "index.jsp", 1);
        }

        [TestMethod]
        public void TestGenerateCaseB()
        {
            NormalGenerateTestTemplate(" ", "/index.jsp", "", "/index.jsp", 1);
        }

        [TestMethod]
        public void TestGenerateCaseC()
        {
            NormalGenerateTestTemplate(" ", "index.jsp", "", "", 1);
        }

        [TestMethod]
        public void TestGenerateCaseD()
        {
            NormalGenerateTestTemplate(" ", "/index.jsp", "", "", 1);
        }

        [TestMethod]
        public void TestGenerateCaseE()
        {
            NormalGenerateTestTemplate(" ", "index.jsp", "", " ", 1);
        }

        [TestMethod]
        public void TestGenerateCaseF()
        {
            NormalGenerateTestTemplate(" ", "/index.jsp", "", " ", 1);
        }

        [TestMethod]
        public void TestGenerateCaseG()
        {
            NormalGenerateTestTemplate(" ", "index.jsp", " ", "", 1);
        }

        [TestMethod]
        public void TestGenerateCaseH()
        {
            NormalGenerateTestTemplate(" ", "/index.jsp", " ", "", 1);
        }

        [TestMethod]
        public void TestGenerateCaseI()
        {
            NormalGenerateTestTemplate("", "index.jsp", "", "", 0);
        }

        [TestMethod]
        public void TestGenerateCaseJ()
        {
            NormalGenerateTestTemplate("", "/index.jsp", "", "", 0);
        }

        [TestMethod]
        public void TestGenerateCaseK()
        {
            NormalGenerateTestTemplate("\n", "index.jsp", "\n", "\n", 1);
        }

        [TestMethod]
        public void TestGenerateCaseL()
        {
            NormalGenerateTestTemplate("\n", "index.jsp", "", "", 1);
        }

        [TestMethod]
        public void TestGenerateCaseM()
        {
            NormalGenerateTestTemplate(" ", "index.jsp", "\n", "", 1);
        }

        [TestMethod]
        public void TestGenerateCaseN()
        {
            NormalGenerateTestTemplate(" ", "index.jsp", "", "\n", 1);
        }

        public Uri GetBaseUri()
        {
            return new Uri("http://webcrawlertest2/");
        }

        public void NormalGenerateTestTemplate(string zero, string one, string two, string three, int expectedCount)
        {
            StringWriter writer = new StringWriter();
            writer.Write(HYPERLINK_TEMPLATE, zero, one, two, three);
            string html = writer.ToString();

            List<string> links = null;
            if (expectedCount == 1)
            {
                links = new List<string>();
                links.Add(new Uri(GetBaseUri(), one).AbsoluteUri);
            }

            NormalGenerateTestTemplate(html, expectedCount, links);
        }

        public void NormalGenerateTestTemplate(string html, int expectedCount)
        {
            NormalGenerateTestTemplate(html, expectedCount, null);
        }

        public void NormalGenerateTestTemplate(string html, int expectedCount, List<string> links)
        {
            Assert.IsTrue(links == null || expectedCount == links.Count);

            Uri baseUri = GetBaseUri();
            HttpRequestResult result = new HttpRequestResult()
            {
                Content = html,
                RequestUrl = baseUri,
                ResultUrl = baseUri
            };


            List<HyperLink> hyperlinks = new HtmlParser(result).GenerateHyperLinks();

            Assert.AreEqual(hyperlinks.Count, expectedCount);

            if (links != null)
            {
                for (int i = 0; i < links.Count; i++)
                {
                    Assert.AreEqual(links[i], hyperlinks[i].AbsoluteUri.ToString());
                }
            }
        }
    }
}
