using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IInteractive.WebTest
{
    [TestClass]
    public class TestHtmlObject
    {
        [TestMethod]
        public void TestEquals()
        {
            var defaultaspx1 = new HyperLink()
                                   {
                                       Path = "/default.aspx",
                                       Root = new Uri("http://about.me/")
                                   };

            var defaultaspx2 = new HyperLink()
                                   {
                                       Path = "default.aspx",
                                       Root = new Uri("http://about.me")
                                   };

            var defaultaspx3 = new HyperLink()
                                   {
                                       Path = "../default.aspx",
                                       Root = new Uri("http://about.me/sub1/")
                                   };

            Assert.AreEqual(defaultaspx1, defaultaspx2);
            Assert.AreEqual(defaultaspx1, defaultaspx3);

            var links = new List<HyperLink>() {defaultaspx1, defaultaspx2, defaultaspx3};

            Assert.AreEqual(1, links.Distinct().Count());
        }
    }
}
