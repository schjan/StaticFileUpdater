using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fclp;

namespace StaticFileUpdater.BuildTool
{
    class Program
    {
        private static bool isSilent;

        static int Main(string[] args)
        {
            Console.WriteLine("StaticFileUpdater BuildTool " + Assembly.GetExecutingAssembly().GetName().Version);

            var p = new FluentCommandLineParser();

            p.Setup<bool>('s', "silent")
                .Callback(silent => isSilent = silent)
                .SetDefault(false)
                .WithDescription("Run without output");

            p.Setup<string>('n', "new")
                .SetDefault("")
                .WithDescription("Directory of new files");

            p.Setup<string>('o', "old")
                .SetDefault("")
                .WithDescription("Directory of old files");

            p.Setup<string>('d', "dir")
                .SetDefault("")
                .WithDescription("Output directory of patch data");


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

            return 0;
        }
    }
}
