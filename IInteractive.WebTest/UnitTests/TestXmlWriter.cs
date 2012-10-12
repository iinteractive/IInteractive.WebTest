using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using IInteractive.WebTest.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestXmlWriter
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

        private TestRunType _testRun = null;

        private const string ResultsXml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<TestRun id=\"570a67dd-14b3-4443-881a-850c376edee9\" name=\"Web Test Allegra.com\" runUser=\"IINTERACTIVE\\builder\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />";

        [TestInitialize]
        public void Initialize()
        {
            _testRun = new TestRunType
                           {
                               id = "570a67dd-14b3-4443-881a-850c376edee9",
                               name = "Web Test Allegra.com",
                               runUser = "IINTERACTIVE\\builder"
                           };
        }

        [TestMethod]
        public void TestFileWrite()
        {
            var file = new FileInfo("results-file.xml");

            if (file.Exists) file.Delete();

            var writer = new WebTestXmlWriter();
            
            

            writer.Write(file.CreateText(), _testRun);
            
            file = new FileInfo("results-file.xml");
            Assert.IsTrue(file.Exists, "File was not created");
            Assert.IsTrue(file.Length > 0, "File is empty");
        }

        [TestMethod]
        public void TestXmlStringWriter()
        {
            var memoryStream = new MemoryStream();
            

            var writer = new WebTestXmlWriter();

            writer.Write(memoryStream, _testRun);

            var actual = Encoding.UTF8.GetString(memoryStream.ToArray());

            Assert.AreEqual(ResultsXml, actual, true);
        }

        [TestMethod]
        public void TestReadRunConfiguration()
        {
            
        }
    }
}
