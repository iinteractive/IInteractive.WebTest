using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IInteractive.WebConsole
{
    public class Program
    {
        public static string HELP_MESSAGE = "Please specify a configuration file in the following format where <config-file-location> is a file location.\n"
            + "\t-config:<config-file-location>\n"
            + "If you specified a config file correctly, please make sure no other arguments are included in the call to this program.";
        public static string CONFIG_FILE_ERROR_MESSAGE = "There was an error parsing the config file.";
        public static string[] CONFIG_ARG_PREFIXES = new string[] { 
            "-config:",
            "-c:",
            "--config:"
        };

        static int Main(string[] args)
        {
            int returnCode = new Program(args).Execute();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            return returnCode;
        }

        public Program(string[] Args)
        {
            this.Args = Args;
        }

        public string[] Args { get; set; }

        public int Execute()
        {
            if (Args.Length == 0)
            {
                Console.WriteLine(HELP_MESSAGE);
                return 1;
            }
            else
            {
                string arg = Args[0];
                string configFile = null;
                foreach (var argPrefix in CONFIG_ARG_PREFIXES)
                {
                    if (arg.StartsWith(argPrefix))
                    {
                        configFile = arg.Substring(argPrefix.Length);
                        break;
                    }
                }

                if (String.IsNullOrEmpty(configFile))
                {
                    Console.WriteLine(HELP_MESSAGE);
                    return 1;
                }
                else
                {
                    Configuration config = null;
                    try
                    {
                        ConfigurationManager.OpenMappedExeConfiguration(
                             new ExeConfigurationFileMap(configFile)
                             , ConfigurationUserLevel.None
                         );
                    }
                    catch(ConfigurationErrorsException ex)
                    {
                        Console.WriteLine(CONFIG_FILE_ERROR_MESSAGE);
                        return 1;
                    }
                    if (config == null)
                    {
                        Console.WriteLine(CONFIG_FILE_ERROR_MESSAGE);
                        return 1;
                    }
                    ConfigurationSectionCollection sections = config.Sections;
                    if (sections == null)
                    {
                        Console.WriteLine(CONFIG_FILE_ERROR_MESSAGE);
                        return 1;
                    }
                    ConfigurationSection section = sections.Get("linkCheckerConfig");
                    if (section == null)
                    {
                        Console.WriteLine(CONFIG_FILE_ERROR_MESSAGE);
                        return 1;
                    }
                    LinkCheckerConfigSection linkSection = (LinkCheckerConfigSection)section;
                    if (linkSection == null)
                    {
                        Console.WriteLine(CONFIG_FILE_ERROR_MESSAGE);
                        return 1;
                    }

                    WebSiteTestSuiteGenerator generator = new WebSiteTestSuiteGenerator(linkSection);
                }
            }

            return 0;
        }
    }
}
