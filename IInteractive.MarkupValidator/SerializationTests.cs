using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;

namespace IInteractive.MarkupValidator
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void TestErrorSerialization()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Error));
            serializer.Serialize(Console.Out, new Error(1, 2, "some message!", 3, "An explanation!", "The source!"));
        }

        [TestMethod]
        public void TestWarningSerialization()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Warning));
            serializer.Serialize(Console.Out, new Warning(1, 2, "some message!"));
        }

        [TestMethod]
        public void TestWarningsSerialization()
        {
        }

        [TestMethod]
        public void TestErrorsSerialization()
        {

        }
    }
}
