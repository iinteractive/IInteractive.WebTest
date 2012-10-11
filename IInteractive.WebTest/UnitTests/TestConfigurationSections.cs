using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestConfigurationSections
    {
        [TestMethod]
        public void TestBrowserConfigSection()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSectionGroupCollection sectionGroups = config.SectionGroups;
            Assert.IsNotNull(sectionGroups);
            ConfigurationSectionGroup group = sectionGroups.Get("browserConfigGroup");
            Assert.IsNotNull(group);
            ConfigurationSection section = group.Sections.Get("browserConfig");
            foreach (var key in group.Sections.Keys)
            {
                Console.WriteLine("key = " + key.ToString());
            }
            Assert.IsNotNull(section);
            BrowserConfigSection browserConfigSection = (BrowserConfigSection)section;
            Assert.IsNotNull(browserConfigSection);

            Assert.IsNotNull(browserConfigSection.Accept);
            Assert.IsNotNull(browserConfigSection.AcceptCharset);
            Assert.IsNotNull(browserConfigSection.AcceptLanguage);
            Assert.IsNotNull(browserConfigSection.AllowAutoRedirect);
            Assert.IsNotNull(browserConfigSection.CurrentConfiguration);
            Assert.IsNotNull(browserConfigSection.ElementInformation);
        }
    }
}
