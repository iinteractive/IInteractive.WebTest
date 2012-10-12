using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebConsole;

namespace IInteractive.WebTest
{
    [TestClass]
    public class TestHtmlObject
    {
        [TestMethod]
        public void TestEquals()
        {
            var defaultaspx1 = new HyperLink(new Uri("http://about.me/"), "/default.aspx");

            var defaultaspx2 = new HyperLink(new Uri("http://about.me/"), "default.aspx");

            var defaultaspx3 = new HyperLink(new Uri("http://about.me/sub1/"), "../default.aspx");

            Assert.IsTrue(defaultaspx1.Equals(defaultaspx2));
            Assert.IsTrue(defaultaspx1.Equals(defaultaspx3));

            var links = new List<HyperLink>() {defaultaspx1, defaultaspx2, defaultaspx3};

            Assert.AreEqual(1, links.Distinct().Count());
        }
    }
}
