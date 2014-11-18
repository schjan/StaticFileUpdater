using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fclp;
using NLog;
using StaticFileUpdater.PatchBuilder;

namespace StaticFileUpdater.BuildTool
{
    class Program
    {
        private static Logger logger = LogManager.GetLogger("CLI");
        private static BuildOptions options;

        static int Main(string[] args)
        {
            Console.WriteLine("StaticFileUpdater BuildTool " + Assembly.GetExecutingAssembly().GetName().Version);
            
            var p = new FluentCommandLineParser();

            p.Setup<bool>('s', "silent")
                .Callback(silent => options.SilentBuild = silent)
                .SetDefault(false)
                .WithDescription("Run without output");

            p.Setup<string>('w', "working")
                .Callback(w => options.WorkingDirectory = w)
                .SetDefault(Environment.CurrentDirectory)
                .WithDescription("Directory of new files");

            //p.Setup<string>('o', "old")
            //    .SetDefault("")
            //    .WithDescription("Directory of old files");

            p.Setup<string>('o', "output")
                .Callback(dir => options.OutputDirectory = dir)
                .SetDefault("")
                .WithDescription("Output directory of patch data");

            p.Setup<string>('c', "config")
                .Callback(c => options.ConfigFileName = c)
                .SetDefault(null)
                .WithDescription("Name of provided config file if not .sfu.yml or sfu.yml");


            p.SetupHelp("h");

            var result = p.Parse(args);

            if (result.HelpCalled)
            {
                Console.WriteLine("\r\nHelp:\r\n");
                Console.WriteLine("Argument\t\tDescription");
                foreach (var v in p.Options)
                {
                    Console.WriteLine("-{0}  --{1}\t\t{2}", v.ShortName, v.LongName, v.Description);
                }
                return 0;
            }

            if (result.HasErrors)
            {
                Console.WriteLine("An error occured parsing your arguments. ");
                return -1;
            }

            if(options.SilentBuild)
                LogManager.GlobalThreshold = LogLevel.Error;

            logger.Trace(() => "Now call Builder");

            var b = new Builder(options);
            b.Build();
            
#if DEBUG
            Console.WriteLine("Press any key to exit program");
            Console.ReadLine();
#endif

            return 0;
        }
    }
}
