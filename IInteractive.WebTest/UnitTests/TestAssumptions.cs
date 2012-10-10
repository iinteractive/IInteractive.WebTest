using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IInteractive.WebTest
{
    [TestClass]
    public class TestAssumptions
    {
        static TestAssumptions()
        {
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public TestAssumptions()
        {
            // TODO: Make real from pseudo code
            // Read in configuration...
            // Override settings for browser
            // URL's to crawl
            // Root url
            // Extra url's
            // End Configuration
            //
            // Start the crawl
            // Root -> Go through all links (internal) -> Web Pages
            // 

            //var webPages = new List<WebPage>();
            //var rootWebPage = new WebPage(new Uri(""));

            //foreach(var link in rootWebPage.Links.Distinct())
            //{
            //    var subPage = new WebPage(link.AbsoluteUri);

            //    if (!webPages.Contains(subPage)) webPages.Add(subPage);
            //}

        }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext context)
        {
        }

        [TestMethod]
        public void UriProofOfConcept()
        {
            var baseUri = new Uri("http://about.me");
            var baseUriEndSlash = new Uri("http://about.me/");
            Assert.AreEqual(baseUri, baseUriEndSlash);

            var defaultUri = new Uri(baseUri, "default.aspx");
            var defaultUriBeginSlash = new Uri(baseUri, "/default.aspx");
            Assert.AreEqual(defaultUri, defaultUriBeginSlash);

            var subdirectoryUri = new Uri(baseUri, "sub1/sub2/default.aspx");
            var subdirectoryUriBeginSlash = new Uri(baseUri, "/sub1/sub2/default.aspx");
            Assert.AreEqual(subdirectoryUri, subdirectoryUriBeginSlash);

            var subdirectoryUriRelative = new Uri(new Uri(baseUri, "/sub1/default.aspx"), "sub2/default.aspx");
            Assert.AreEqual(subdirectoryUri, subdirectoryUriRelative);

            var subdirectoryUriLocal = new Uri(new Uri(baseUri, "/sub1/default.aspx"), "../sub1/sub2/default.aspx");
            Assert.AreEqual(subdirectoryUri, subdirectoryUriLocal);

            var subdirectoryUriRoot = new Uri(new Uri(baseUri, "/sub3/default.aspx"), "/sub1/sub2/default.aspx");
            Assert.AreEqual(subdirectoryUri, subdirectoryUriRoot);

            var relativeUri = new Uri("/default.aspx", UriKind.RelativeOrAbsolute);
            Assert.IsFalse(relativeUri.IsAbsoluteUri);

            relativeUri = new Uri("/sub1/sub2/default.aspx", UriKind.RelativeOrAbsolute);
            Assert.IsFalse(relativeUri.IsAbsoluteUri);

            var absoluteUri = new Uri("http://www.microsoft.com/", UriKind.RelativeOrAbsolute);
            Assert.IsTrue(absoluteUri.IsAbsoluteUri);
            
            absoluteUri = new Uri("http://microsoft.com/sub1/sub2", UriKind.RelativeOrAbsolute);
            Assert.IsTrue(absoluteUri.IsAbsoluteUri);

            absoluteUri = new Uri("http://microsoft.com/sub1/sub2/", UriKind.RelativeOrAbsolute);
            Assert.IsTrue(absoluteUri.IsAbsoluteUri);

            absoluteUri = new Uri("http://microsoft.com/sub1/sub2/default.aspx", UriKind.RelativeOrAbsolute);
            Assert.IsTrue(absoluteUri.IsAbsoluteUri);

            Assert.AreEqual(absoluteUri.Host, "microsoft.com");
            Console.WriteLine("{0}, {1}", "AbsolutePath", absoluteUri.AbsolutePath);
            Console.WriteLine("{0}, {1}", "AbsoluteUri", absoluteUri.AbsoluteUri);
            Console.WriteLine("{0}, {1}", "Authority", absoluteUri.Authority);
            Console.WriteLine("{0}, {1}", "DnsSafeHost", absoluteUri.DnsSafeHost);
            Console.WriteLine("{0}, {1}", "Fragment", absoluteUri.Fragment);
            Console.WriteLine("{0}, {1}", "Host", absoluteUri.Host);
            Console.WriteLine("{0}, {1}", "HostNameType", absoluteUri.HostNameType);
            Console.WriteLine("{0}, {1}", "LocalPath", absoluteUri.LocalPath);
            Console.WriteLine("{0}, {1}", "OriginalString", absoluteUri.OriginalString);
            Console.WriteLine("{0}, {1}", "PathAndQuery", absoluteUri.PathAndQuery);
            Console.WriteLine("{0}, {1}", "Port", absoluteUri.Port);
            Console.WriteLine("{0}, {1}", "Query", absoluteUri.Query);
            Console.WriteLine("{0}, {1}", "Scheme", absoluteUri.Scheme);
            Console.WriteLine("{0}, {1}", "Segments", absoluteUri.Segments);

            Console.WriteLine("Needed: " + absoluteUri.Scheme + "://" + absoluteUri.Host + (absoluteUri.IsDefaultPort ? "" : (":" + absoluteUri.Port)));
        }

        [TestMethod]
        public void SortedSetUniqueness()
        {
            SortedSet<string> set = new SortedSet<string>();
            set.Add("val");
            int count = set.Count;
            set.Add("val");
            int count2 = set.Count;
            Assert.AreEqual(count, count2);
        }

    }
}
