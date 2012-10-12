using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IInteractive.WebConsole.Results
{
    public class WebTestXmlWriter
    {
        private static readonly Encoding Utf8EncodingWithNoByteOrderMark;

        static WebTestXmlWriter()
        {
            Utf8EncodingWithNoByteOrderMark = new UTF8Encoding(false);
        }

        private readonly XmlSerializer _serializer;
        
        public WebTestXmlWriter()
        {
            _serializer = new XmlSerializer(typeof(TestRunType));//, new XmlRootAttribute("TestRun") { Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010" });
            //_serializerNamespaces = new XmlSerializerNamespaces();
        }

        public void Write(Stream stream, TestRunType testRun)
        {
            var xmlWriterSettings = new XmlWriterSettings { Encoding = Utf8EncodingWithNoByteOrderMark, Indent = true };

            var xmlWriter = XmlWriter.Create(stream, xmlWriterSettings);
            Write(xmlWriter, testRun);
        }

        public void Write(TextWriter writer, TestRunType testRun)
        {
            var xmlWriterSettings = new XmlWriterSettings { Encoding = Utf8EncodingWithNoByteOrderMark, Indent = true };

            var xmlWriter = XmlWriter.Create(writer, xmlWriterSettings);

            Write(xmlWriter, testRun);
        }

        public void Write(XmlWriter writer, TestRunType testRun)
        {
            try
            {
                _serializer.Serialize(writer, testRun, new XmlSerializerNamespaces(new XmlQualifiedName[]
                                                                                       {
                                                                                           new XmlQualifiedName(string.Empty, "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")
                                                                                       }));
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
