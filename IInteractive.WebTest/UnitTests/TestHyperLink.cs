using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace IInteractive.WebTest
{
    [TestClass]
    public class TestHyperLink
    {
        public static string[] HOSTS = new string[] {
            "remotesite.iinteractive.com"
        };
        public static string[] PORTS = new string[] {
            "",
            ":1212"
        };
        public static string[] PROTOCOLS = new string[] {
            "http://",
            "https://"
        };
        public static string[] PATHS = new string[] {
            "",
            "/",
            "/index.jsp",
            "/folder/",
            "/folder/index.jsp"
        };
        
        public static string[] QUERY_STRINGS = new string[] {
            "",
            "?param1=true"
        };

        public static string[] HASH_TAGS = new string[] {
            "",
            "#hashtag"
        };

        private static string[] _URLS {
            get {
                if(_URLS == null) {
                    _URLS = new string[HOSTS.Length * PORTS.Length * PROTOCOLS.Length * PATHS.Length * QUERY_STRINGS.Length * HASH_TAGS.Length];
                    int ctr = 0;
                    foreach(string host in HOSTS) {
                        foreach(string port in PORTS) {
                            foreach(string protocol in PROTOCOLS) {
                                foreach(string path in PATHS) {
                                    foreach(string queryString in QUERY_STRINGS) {
                                        foreach(string hashTag in HASH_TAGS) {
                                            _URLS[ctr] = protocol + host + port + path + queryString + hashTag;
                                            ctr++;
                                        }
                                    }
                                
                                }
                            }
                        }
                    }
                }

                return _URLS;
            }
            set
            {
                _URLS = value;
            }
        }

        public static string[] URLS {
            get
            {
                return _URLS;
            }
        }

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
            List<HyperLink> hyperlinks = HyperLink.Generate(baseUri, html);

            Assert.IsTrue(hyperlinks.Count == expectedCount);

            if (links != null)
            {
                for (int i = 0; i < links.Count; i++)
                {
                    Assert.AreEqual(links[i], hyperlinks[i].AbsoluteUri);
                }
            }
        }
    }
}
