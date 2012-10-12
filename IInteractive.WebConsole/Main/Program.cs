using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace IInteractive.WebConsole
{
    public class Program
    {
        public static string HELP_MESSAGE = "Please specify a configuration file in the following format where <config-file-location> is a file location.\n"
            + "\t-config:<config-file-location>\n"
            + "If you specified a config file correctly, please make sure no other arguments are included in the call to this program.";
        public static string MISSING_CONFIG_FILE_MESSAGE = "The specified config file, \"{0}\" does not exist.";
        public static string CONFIG_FILE_ERROR_MESSAGE = "There was an error parsing the config file.";
        public static string MISSING_CONFIG_SECTIONS_MESSAGE = "The config file specified is missing the configSections element.";
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
            else if (Args.Length != 1)
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
                    if (!File.Exists(configFile))
                    {
                        Console.WriteLine(MISSING_CONFIG_FILE_MESSAGE, configFile);
                        return 1;
                    }

                    Configuration config = null;
                    try
                    {
                        ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                        map.ExeConfigFilename = configFile;
                        config = ConfigurationManager.OpenMappedExeConfiguration(
                                         map
                                         , ConfigurationUserLevel.None
                                     );
                    }
                    catch(ConfigurationErrorsException)
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
                    LinkCheckerConfigSection section = null;
                    try
                    {
                        section = (LinkCheckerConfigSection)sections.Get("linkCheckerConfig");
                    }
                    catch (InvalidCastException)
                    {
                        Console.WriteLine(MISSING_CONFIG_SECTIONS_MESSAGE);
                        return 1;
                    }
                    catch (ConfigurationErrorsException)
                    {
                        Console.WriteLine(CONFIG_FILE_ERROR_MESSAGE);
                        return 1;
                    }
                    if (section == null)
                    {
                        Console.WriteLine(CONFIG_FILE_ERROR_MESSAGE);
                        return 1;
                    }

                    WebSiteTestSuiteGenerator generator = new WebSiteTestSuiteGenerator(section);
                }
            }

            return 0;
        }
    }
}
