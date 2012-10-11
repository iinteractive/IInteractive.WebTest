using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IInteractive.WebTest.Results
{
    public class WebTestXmlWriter
    {
        private static readonly Encoding _utf8EncodingWithNoByteOrderMark;

        static WebTestXmlWriter()
        {
            _utf8EncodingWithNoByteOrderMark = new UTF8Encoding(false);
        }

        private readonly XmlSerializer _serializer;
        
        public WebTestXmlWriter()
        {
            _serializer = new XmlSerializer(typeof(TestRun), new XmlRootAttribute("TestRun") { Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010" });
            //_serializerNamespaces = new XmlSerializerNamespaces();
        }

        public void Write(Stream stream, TestRun testRun)
        {
            var xmlWriterSettings = new XmlWriterSettings { Encoding = _utf8EncodingWithNoByteOrderMark, Indent = true };

            var xmlWriter = XmlWriter.Create(stream, xmlWriterSettings);
            Write(xmlWriter, testRun);
        }

        public void Write(TextWriter writer, TestRun testRun)
        {
            var xmlWriterSettings = new XmlWriterSettings { Encoding = _utf8EncodingWithNoByteOrderMark, Indent = true };

            var xmlWriter = XmlWriter.Create(writer, xmlWriterSettings);

            Write(xmlWriter, testRun);
        }

        public void Write(XmlWriter writer, TestRun testRun)
        {
            try
            {
                _serializer.Serialize(writer, testRun, testRun.Namespaces);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (writer != null)
                {
                    try
                    {
                        writer.Close();
                    }
                    catch { }
                }
            }
        }
    }
}
