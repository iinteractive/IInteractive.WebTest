using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebConsole;

namespace IInteractive.WebTest
{
    [TestClass]
    public class TestHyperLink
    {
        public static string HYPERLINK_TEMPLATE = "<a{0}href=\"{1}\"{2}>{3}</a>";
        
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
            return new Uri("http://remotesite.iinteractive.com/");
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
