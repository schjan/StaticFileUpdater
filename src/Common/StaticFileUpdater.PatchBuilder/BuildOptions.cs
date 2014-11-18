using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticFileUpdater.PatchBuilder
{
    public struct BuildOptions
    {
        public string WorkingDirectory;

        public string ConfigFileName;

        public bool SilentBuild;

        public string OutputDirectory;
    }
}
