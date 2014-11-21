using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        public PatchFiles BuildPatchLocal()
        {
            logger.Debug(() => "Start building patch local");

            return CheckFiles();
        }

        protected PatchFiles CheckFiles()
        {
            if (string.IsNullOrWhiteSpace(_options.LastPatchDirectory) || !Directory.Exists(_options.LastPatchDirectory))
            {
                logger.Error(() => "If patch is build local, LastPatchDirectory is needed");
                throw new ArgumentNullException("LastPatchDirectory");
            }

            var patchFiles = new PatchFiles();
            var newFiles = GetAllFilesOfDirRecursive(_options.WorkingDirectory);
            var oldFiles = GetAllFilesOfDirRecursive(_options.LastPatchDirectory).ToList();
            var comparer = new FileComparer();

            foreach (var file in newFiles)
            {
                if (oldFiles.Contains(file))
                {
                    if (!comparer.FilesAreEqual(Path.Combine(_options.WorkingDirectory, file),
                        Path.Combine(_options.LastPatchDirectory, file)))
                        patchFiles.Updated.Add(file);
                }
                else
                {
                    patchFiles.Added.Add(file);
                }
                oldFiles.Remove(file);
            }

            patchFiles.Deleted = oldFiles;

            return patchFiles;
        }

        protected IEnumerable<string> GetAllFilesOfDirRecursive(string directory)
        {
            var skipDirectory = directory.Length;

            if (!directory.EndsWith("" + Path.DirectorySeparatorChar))
                skipDirectory++;

            return Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories)
                .Select(f => f.Substring(skipDirectory));
        }

        private IEnumerable<string> GetAllFilesOfDir(string directory)
        {
            return Directory.GetFiles(directory, "", SearchOption.AllDirectories);
        }
    }
}
