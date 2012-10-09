using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IInteractive.WebTest
{
    [TestClass]
    public class TestWebPage
    {
        [TestMethod]
        public void TestNormalWebPageGet()
        {
            Console.WriteLine("METHOD ");

            var page = new WebPage(new Uri("http://www.google.com/"), new Browser());

            page.Get();

            Assert.IsTrue(page.Links.Count > 0, "No HyperLinks found!");
            Assert.IsTrue(page.Images.Count > 0, "No Images found!");
            Assert.IsTrue(page.JavaScripts.Count > 0, "No Scripts found!");
            Assert.IsNotNull(page.Path, "Unknown Path!");

            IEnumerable<HttpValidationError> errors;



            var isFailure = page.Validate(out errors);

            string errorMessage = "\r\n";

            foreach (var item in errors)
            {
                errorMessage += string.Format("Url: {0}\r\nCode: {1}, Message: {2}\r\nStack Trace:\r\n{3}\r\n\r\n", item.AbsoluteUri, item.HttpCode, item.Message, item.Error.StackTrace);
            }

            Assert.IsTrue(isFailure, errorMessage);
            Assert.IsFalse(errors.Any());
        }

        [TestMethod]
        public void TestAbnormalWebPageGet()
        {
            // The uri needs to be an image, or something with a content type besides html.
            var page = new WebPage(new Uri("http://www.google.com/images/srpr/logo3w.png"), new Browser());

            page.Get();

            Assert.IsTrue(page.Links.Count == 0, "Hyperlinks found!");
            Assert.IsTrue(page.Images.Count == 0, "Images found!");
            Assert.IsTrue(page.JavaScripts.Count == 0, "Scripts found!");
            Assert.IsNotNull(page.Path, "Unknown Path!");

            IEnumerable<HttpValidationError> errors;
            var isFailure = page.Validate(out errors);

            string errorMessage = "\r\n";
            foreach (var item in errors)
            {
                errorMessage += string.Format("Url: {0}\r\nCode: {1}, Message: {2}\r\nStack Trace:\r\n{3}\r\n\r\n", item.AbsoluteUri, item.HttpCode, item.Message, item.Error.StackTrace);
            }

            Assert.IsTrue(isFailure, errorMessage);
            Assert.IsFalse(errors.Any());
        }
    }
}
