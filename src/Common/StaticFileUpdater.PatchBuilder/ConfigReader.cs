using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace StaticFileUpdater.PatchBuilder
{
    public class ConfigReader
    {
        private static Logger logger = LogManager.GetLogger("ConfigReader");
        private string _filePath;


        public void Load(string filePath)
        {
            logger.Debug(() => "Load Configfile: " + filePath);
            _filePath = filePath;
        }
    }
}
