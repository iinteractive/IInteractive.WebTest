using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.MarkupValidator;
using IInteractive.WebConsole;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestHtmlValidate
    {
        [TestMethod]
        public void TestConstructor()
        {
            var validator = new HtmlValidator(new Uri(TestCrawler.GetTestUrl("/")));
        }

    }
}
