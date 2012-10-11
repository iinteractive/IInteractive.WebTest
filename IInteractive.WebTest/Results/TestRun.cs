using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.Common;

namespace IInteractive.WebTest.Results
{
    [Serializable]
    //[XmlRoot("TestRun", Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010", IsNullable = false)]
    [XmlRoot("TestRun", Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010", IsNullable = false)]
    public class TestRun
    {
        public TestRun()
        {
            _namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[]
                                                          {
                                                              new XmlQualifiedName(string.Empty, "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")
                                                              //new XmlQualifiedName(string.Empty, "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")
                                                              //new XmlQualifiedName(string.Empty, string.Empty)
                                                          });

        }
        [XmlAttribute(AttributeName = "id")]
        public Guid Id { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "runUser")]
        public string RunUser { get; set; }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces
        {
            get { return this._namespaces; }
        }
        private readonly XmlSerializerNamespaces _namespaces;
    }
}
