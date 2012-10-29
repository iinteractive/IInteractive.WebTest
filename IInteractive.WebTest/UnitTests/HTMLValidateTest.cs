using IInteractive.MarkupValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace DoctypeEncodingValidateUnitTests
{
    /// <summary>
    ///This is a test class for HTMLValidateTest and is intended
    ///to contain all HTMLValidateTest Unit Tests
    ///</summary>
    /// <remarks>
    /// Author: María Eugenia Fernández Menéndez
    /// E-mail: mariae.fernandez.menendez@gmail.com
    /// Date created: 13-04-2009
    /// Last modified: 03-05-2009
    /// Version: 0.1
    /// License:
    /// 
    ///     This file is part of IInteractive.MarkupValidator.
    ///     
    ///     IInteractive.MarkupValidator is free software: you can redistribute
    ///     it and/or modify it under the terms of the GNU General Public 
    ///     License as published by the Free Software Foundation, either 
    ///     version 3 of the License, or (at your option) any later version.
    ///     
    ///     IInteractive.MarkupValidator is distributed in the hope that it 
    ///     will be useful, but WITHOUT ANY WARRANTY; without even the 
    ///     implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
    ///     PURPOSE. See the GNU General Public License for more details.
    ///  
    ///     You should have received a copy of the GNU General Public License
    ///     along with IInteractive.MarkupValidator. If not, see <http://www.gnu.org/licenses/>.
    ///     
    /// </remarks>
    [TestClass()]
    public class HTMLValidateTest
    {
        #region variables
        private TestContext testContextInstance;
        /// <summary>
        /// List that contains "not found" urls to validate with the method ValidationOfURL
        /// </summary>
        private List<string> notFoundURLs = new List<string>() { "htpp://www.walidator.org", "http://www.del.icio.us/", "http://www.nintendo.org/" };
        /// <summary>
        /// List that contains valid urls to validate with the method Validate.
        /// </summary>
        private List<string> validURLs = new List<string>() { "http://mariaefm.net/docs_pfc/xhtml_11.html", "http://mariaefm.net/docs_pfc/xhtml_10.html", "http://mariaefm.net/docs_pfc/xhtml_mp_12.html" };
        /// <summary>
        /// List that contains invalid urls to validate with the method Validate.
        /// </summary>
        private List<string> invalidURLs = new List<string>() { "http://mariaefm.net/docs_pfc/Nuevo.html", "http://www.google.es", "http://www.google.com", };
        /// <summary>
        /// List that contains valid UTF-8 urls to validate with the method ValidationOfEncoding
        /// </summary>
        private List<string> validUTF8URLs = new List<string>() { "http://www.google.com", "http://www.deliciou.com" };
        /// <summary>
        /// List that contains invalid UTF-8 urls to validate with the method ValidationOfEncoding
        /// </summary>
        private List<string> invalidUTF8URLs = new List<string>() { "http://mariaefm.net/docs_pfc/Nuevo.html", "http://www.google.es" };
        /// <summary>
        /// List that contains valid XHTML Basic 1.1 urls to validate with the method ValidationOfHTML
        /// </summary>
        private List<string> xhtml11ValidURLs = new List<string>() { "http://mariaefm.net/docs_pfc/xhtml_11.html" };
        /// <summary>
        /// List that contains invalid XHTML Basic 1.1 urls to validate with the method ValidationOfHTML
        /// </summary>
        private List<string> xhtml11InvalidURLs = new List<string>() { "http://mariaefm.net/docs_pfc/xhtml_11_invalid.html" };
        /// <summary>
        /// List that contains valid XHTML Basic 1.0 urls to validate with the method ValidationOfHTML
        /// </summary>
        private List<string> xhtml10ValidURLs = new List<string>() { "http://mariaefm.net/docs_pfc/xhtml_10.html" };
        /// <summary>
        /// List that contains invalid XHTML Basic 1.0 urls to validate with the method ValidationOfHTML
        /// </summary>
        private List<string> xhtml10InvalidURLs = new List<string>() { "http://mariaefm.net/docs_pfc/xhtml_10_invalid.html" };
        /// <summary>
        /// List that contains valid XHTML Mobile Profile 1.2 urls to validate with the method ValidationOfHTML
        /// </summary>
        private List<string> xhtmlMP12ValidURLs = new List<string>() { "http://mariaefm.net/docs_pfc/xhtml_mp_12.html" };
        /// <summary>
        /// List that contains invalid XHTML Mobile Profile 1.2 urls to validate with the method ValidationOfHTML
        /// </summary>
        private List<string> xhtmlMP12InvalidURLs = new List<string>() { "http://mariaefm.net/docs_pfc/xhtml_mp_12_invalid.html" };
        #endregion

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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for the method ValidationOfURL with "not found" urls.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidationOfURLTest()
        {
            //Instance of HTMLValidate class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;
            //Iterate over the notFoundURLs list.
            foreach (string url in notFoundURLs)
            {
                //For each url, initialize the object with the url.
                htmlValidate = new HTMLValidate(url);
                //Try to validate the url
                try
                {
                    htmlValidate.ValidationOfURL();
                }
                catch (ControlException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.errorTimeStamp);
                    Assert.Fail("Url NOT found");
                }
            }
        }

        /// <summary>
        ///A test for the method Validate with valid urls
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidateValidURLsTest()
        {
            //Instance of HTMLValidate class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;
            //Iterate over the validURLs list
            foreach (string url in validURLs)
            {
                //For each url, initialize the object with the url.
                htmlValidate = new HTMLValidate(url);
                //We expect that the test returns true. Are valid urls.
                bool expected = true;
                //The actual value obtained by calling the method to validate
                bool actual = htmlValidate.Validate(HTMLValidate.emptyEncoding, HTMLValidate.emptyDoctype);
                //This is the result of the test with a message of what we are expecting.
                Assert.AreEqual(expected, actual, url + ". This url should be a valid url");
            }
        }

        /// <summary>
        ///A test for the method Validate with invalid urls
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidateInvalidURLsTest()
        {
            //Instance of HTMLValidate class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;
            //Iterate over the invalidURLs list
            foreach (string url in invalidURLs)
            {
                //For each url, initialize the object with the url.
                htmlValidate = new HTMLValidate(url);
                //We expect that the test returns false. Aren't valid urls.
                bool expected = false;
                //The actual value obtained by calling the method to validate
                bool actual = htmlValidate.Validate(HTMLValidate.emptyEncoding, HTMLValidate.emptyDoctype);
                //This is the result of the test with a message of what we are expecting.
                Assert.AreEqual(expected, actual, url + ". This url should not be a valid url");
            }
        }

        /// <summary>
        ///A test for the method ValidationOfEncoding with valid UTF-8 urls
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidationOfEncodingValidTest()
        {
            //Instance of HTMLValidate class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;

            //Iterate over the validUTF8URLs list
            foreach (string url in validUTF8URLs)
            {
                //For each url, initialize the object with the url.
                htmlValidate = new HTMLValidate(url);
                //We expect that the test returns true. Are valid urls.
                bool expected = true;
                //The actual value obtained by calling the method to validate
                bool actual = htmlValidate.ValidationOfEncoding(HTMLValidate.UTF8);
                //This is the result of the test with a message of what we are expecting.
                Assert.AreEqual(expected, actual, url + ". This url should be a valid utf-8 document.");
            }
        }

        /// <summary>
        ///A test for ValidationOfUtf8 with urls that should not validate.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidationOfEncodingInvalidTest()
        {
            //Instance of HTMLUTF8Validation class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;

            //Iterate over the invalidUTF8URLs list
            foreach (string url in invalidUTF8URLs)
            {
                //For each url, initialize the object with it.
                htmlValidate = new HTMLValidate(url);
                //We expect that the test returns false. Aren't valid urls.
                bool expected = false;
                //The actual value obtained by calling the method to validate
                bool actual = htmlValidate.ValidationOfEncoding(HTMLValidate.UTF8);
                //This is the result of the test with a message of what we are expecting.
                Assert.AreEqual(expected, actual, url + ". This url should not be a valid utf-8 document.");
            }
        }

        /// <summary>
        ///A test for ValidationOfHTML over XHTML Basic 1.1 with urls that should validate.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidationOfHTMLTestOverXHTMLBasic1_1ValidURLs()
        {
            //Instance of HTMLUTF8Validation class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;

            //Iterate over the xhtml11ValidURLs list
            foreach (string url in xhtml11ValidURLs)
            {
                //For each url, initialize the object with it.
                htmlValidate = new HTMLValidate(url);
                //We expect that the test returns true. Are valid urls.
                bool expect = true;
                //The actual value obtained by calling the method to validate
                bool actual = htmlValidate.ValidationOfDoctype(HTMLValidate.XHTML_BASIC_11);
                //This is the result of the test with a message of what we are expecting.
                Assert.AreEqual(expect, actual, url + ". This url should be a valid XHML Basic 1.1 document");
            }
        }

        /// <summary>
        ///A test for ValidationOfHTML over XHTML Basic 1.1 with urls that should not validate.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidationOfHTMLTestOverXHTMLBasic1_1InvalidURLs()
        {
            //Instance of HTMLUTF8Validation class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;

            //Iterate over the xhtml11InvalidURLs list
            foreach (string url in xhtml11InvalidURLs)
            {
                //For each url, initialize the object with it.
                htmlValidate = new HTMLValidate(url);
                //We expect that the test returns false. Aren't valid urls.
                bool expect = false;
                //The actual value obtained by calling the method to validate
                bool actual = htmlValidate.ValidationOfDoctype(HTMLValidate.XHTML_BASIC_11);
                //This is the result of the test with a message of what we are expecting.
                Assert.AreEqual(expect, actual, url + ". This url should not be a valid XHML Basic 1.1 document");
            }
        }

        /// <summary>
        ///A test for ValidationOfHTML over XHTML Basic 1.0 with urls that should validate.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidationOfHTMLTestOverXHTMLBasic1_0ValidURLs()
        {
            //Instance of HTMLUTF8Validation class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;

            //Iterate over the xhtml10ValidURLs list
            foreach (string url in xhtml10ValidURLs)
            {
                //For each url, initialize the object with it.
                htmlValidate = new HTMLValidate(url);
                //We expect that the test returns true. Are valid urls.
                bool expect = true;
                //The actual value obtained by calling the method to validate
                bool actual = htmlValidate.ValidationOfDoctype(HTMLValidate.XHTML_BASIC_10);
                //This is the result of the test with a message of what we are expecting.
                Assert.AreEqual(expect, actual, url + ". This url should be a valid XHML Basic 1.0 document");
            }
        }

        /// <summary>
        ///A test for ValidationOfHTML over XHTML Basic 1.0 with urls that should not validate.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidationOfHTMLTestOverXHTMLBasic1_0InvalidURLs()
        {
            //Instance of HTMLUTF8Validation class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;

            //Iterate over the xhtml10InvalidURLs list
            foreach (string url in xhtml10InvalidURLs)
            {
                //For each url, initialize the object with it.
                htmlValidate = new HTMLValidate(url);
                //We expect that the test returns false. Aren't valid urls.
                bool expect = false;
                //The actual value obtained by calling the method to validate
                bool actual = htmlValidate.ValidationOfDoctype(HTMLValidate.XHTML_BASIC_10);
                //This is the result of the test with a message of what we are expecting.
                Assert.AreEqual(expect, actual, url + ". This url should not be a valid XHML Basic 1.0 document");
            }
        }

        /// <summary>
        ///A test for ValidationOfHTML over XHTML Mobile Profile 1.2 with urls that should validate.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidationOfHTMLTestOverXHTMLMP1_2ValidURLs()
        {
            //Instance of HTMLUTF8Validation class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;

            //Iterate over the xhtmlMP12ValidURLs list
            foreach (string url in xhtmlMP12ValidURLs)
            {
                //For each url, initialize the object with it.
                htmlValidate = new HTMLValidate(url);
                //We expect that the test returns true. Are valid urls.
                bool expect = true;
                //The actual value obtained by calling the method to validate
                bool actual = htmlValidate.ValidationOfDoctype(HTMLValidate.XHTML_MP_12);
                //This is the result of the test with a message of what we are expecting.
                Assert.AreEqual(expect, actual, url + ". This url should be a valid XHML MP 1.2 document");
            }
        }

        /// <summary>
        ///A test for ValidationOfHTML over XHTML Mobile Profile 1.2 with urls that should not validate.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("IInteractive.MarkupValidator.exe")]
        public void ValidationOfHTMLTestOverXHTMLMP1_2InvalidURLs()
        {
            //Instance of HTMLUTF8Validation class.
            IInteractive.MarkupValidator.HTMLValidate htmlValidate;

            //Iterate over the xhtmlMP12InvalidURLs list
            foreach (string url in xhtmlMP12InvalidURLs)
            {
                //For each url, initialize the object with it.
                htmlValidate = new HTMLValidate(url);
                //We expect that the test returns false. Aren't valid urls.
                bool expect = false;
                //The actual value obtained by calling the method to validate
                bool actual = htmlValidate.ValidationOfDoctype(HTMLValidate.XHTML_MP_12);
                //This is the result of the test with a message of what we are expecting.
                Assert.AreEqual(expect, actual, url + ". This url should not be a valid XHML MP 1.2 document");
            }
        }
    }
}
