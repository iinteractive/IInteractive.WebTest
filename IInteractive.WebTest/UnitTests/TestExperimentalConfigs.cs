using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IInteractive.WebTest.Properties;
using System.IO;
using System.Configuration;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestExperimentalConfigs
    {
        [TestMethod]
        public void TestParents()
        {
            string testA = string.Format(Resources.TestA, 1, "test", 3);

            Configuration config = RetrieveConfig(testA);

            ConfSec sec = (ConfSec)config.GetSection("confSec");
            ConfColl coll = sec.ConfColl;
            ConfElem elem = coll[0];

            Assert.AreEqual(sec.Prop, 1);
            Assert.AreNotEqual(sec.Prop, default(int));

            Assert.AreEqual("test", elem.Name);
            Assert.AreEqual(3, elem.Prop);
            Assert.AreEqual(1, elem.GlobalProp);
            Assert.AreNotEqual(default(int), elem.Prop);
        }

        private static Configuration RetrieveConfig(string contents)
        {
            string fileName = Path.GetTempFileName();
            Console.WriteLine("fileName = {0}", fileName);
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(contents);
                writer.Flush();
                writer.Close();
            }

            Console.WriteLine("CONTENTS-------------------------\n{0}\n/CONTENTS------------------------", contents);
            using (StreamReader reader = new StreamReader(fileName))
            {
                string fileContents = reader.ReadToEnd();
                Console.WriteLine("FILE CONTENTS-------------------------\n{0}\n/FILE CONTENTS------------------------", fileContents);
            }
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = fileName;
            return ConfigurationManager.OpenMappedExeConfiguration(
                                map
                                , ConfigurationUserLevel.None
                            );
        }

        [TestMethod]
        public void TestCallbackValidator()
        {
            string testB = string.Format(Resources.TestB, 1, "test", 3);
            Configuration config = RetrieveConfig(testB);

            ConfSec sec = null;
            try
            {
                sec = (ConfSec)config.GetSection("confSec");
                
            }
            catch (ConfigurationErrorsException ex)
            {
            }
            if (sec != null)
            {
                ConfColl coll = sec.ConfColl;
                ConfElem elem = coll[0];
            }
            
        }
    }

    
}
