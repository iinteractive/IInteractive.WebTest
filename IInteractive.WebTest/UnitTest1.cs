using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace IInteractive.WebTest
{
    [TestClass]
    public class UnitTest1
    {
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

        public UnitTest1()
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

        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("METHOD ");

            var page = new WebPage(new Uri("http://www.reallifecomics.com/"));

            page.Get();

            Assert.IsTrue(page.Links.Count > 0, "No HyperLinks found!");
            Assert.IsTrue(page.Images.Count > 0, "No Images found!");
            Assert.IsTrue(page.JavaScripts.Count > 0, "No Scripts found!");
            Assert.IsNotNull(page.Path, "Unknown Path!");

            IEnumerable<HttpValidationError> errors;

            var isFailure = page.Validate(out errors);

            string errorMessage = "\r\n";
            
            foreach(var item in errors)
            {
                errorMessage += string.Format("Url: {0}\r\nCode: {1}, Message: {2}\r\nStack Trace:\r\n{3}\r\n\r\n", item.AbsoluteUri, item.HttpCode, item.Message, item.Error.StackTrace);
            }

            Assert.IsTrue(isFailure, errorMessage);
            Assert.IsFalse(errors.Any());
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
        }

    }
}
