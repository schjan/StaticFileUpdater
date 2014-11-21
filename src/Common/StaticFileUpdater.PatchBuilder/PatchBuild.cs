using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using StaticFileUpdater.Common;

namespace StaticFileUpdater.PatchBuilder
{
    public class PatchBuild
    {
        private static readonly Logger logger = LogManager.GetLogger("PatchBuilder");
        private readonly BuildOptions _options;

        public PatchBuild(BuildOptions options)
        {
            _options = options;
        }

        public void BuildPatchLocal()
        {
            logger.Debug(() => "Start building patch local");

            var patchFiles = new PatchFiles();
            var files = GetAllFilesOfDirRecursive(_options.WorkingDirectory);


        }

        protected IEnumerable<string> GetAllFilesOfDirRecursive(string directory)
        {
            return Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            //foreach (var str in GetAllFilesOfDir(directory))
            //{
            //    yield return str;
            //}

            //foreach (var dir in Directory.GetDirectories(directory))
            //{
            //    foreach (var str in GetAllFilesOfDirRecursive(directory))
            //    {
            //        yield return str;
            //    }
            //}
        }

        private IEnumerable<string> GetAllFilesOfDir(string directory)
        {
            return Directory.GetFiles(directory, "", SearchOption.AllDirectories);
        }
    }
}
