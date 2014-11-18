using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace StaticFileUpdater.PatchBuilder
{
    public class Builder
    {
        private static Logger logger = LogManager.GetLogger("Builder");
        private BuildOptions op;
        private ConfigReader configReader; // wird spaeter Interface.

        public Builder()
        {
            op = new BuildOptions();
            configReader = new ConfigReader();
        }

        public Builder(BuildOptions options /*, IConfigReader configReader = null*/)
        {
            op = options;
            configReader = new ConfigReader();
        }

        /// <summary>
        /// Builds the Patchfiles.
        /// </summary>
        /// <returns>0 if everything went well.</returns>
        public int Build()
        {
            logger.Debug(() => "Pre-Build started");
            logger.Debug(() => "Working Directory: " + op.WorkingDirectory);

            if (string.IsNullOrWhiteSpace(op.WorkingDirectory) || !Directory.Exists(op.WorkingDirectory))
            {
                logger.Fatal("WorkingDirectory not set or directory doesn't exist.");
                return -1;
            }

            var ret = LoadConfigYml();
            if (ret != 0)
                return ret;

            ret = CheckConfig();
            if (ret != 0)
                return ret;

            return 0;
        }

        private int LoadConfigYml()
        {
            var r = new ConfigReader();
            // Check if .sfu.yml / sfu.yml is present in Working Directory
            if (!string.IsNullOrWhiteSpace(op.ConfigFileName))
            {
                var path = Path.Combine(op.WorkingDirectory, op.ConfigFileName);
                if (!File.Exists(path))
                {
                    logger.Fatal("Couldn't find given config .yml file.");
                    return -1;
                }
                r.Load(path);
            }
            else if (File.Exists(Path.Combine(op.WorkingDirectory, "/.sfu.yml")))
            {
                r.Load(Path.Combine(op.WorkingDirectory, "/.sfu.yml"));
            }
            else if (File.Exists(Path.Combine(op.WorkingDirectory, "/sfu.yml")))
            {
                r.Load(Path.Combine(op.WorkingDirectory, "/sfu.yml"));
            }
            else
                logger.Debug(() => "No Configfile found in Workingdir.");

            return 0;
        }

        private int CheckConfig()
        {
            if (string.IsNullOrWhiteSpace(op.WorkingDirectory))
            {
                logger.Fatal("Workingdirectory can't be empty!");
                return -1;
            }
            if (string.IsNullOrWhiteSpace(op.OutputDirectory))
            {
                logger.Fatal("Workingdirectory can't be empty (for now)!");
                return -1;
            }
            

            return 0;
        }
    }
}
