using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticFileUpdater.PatchBuilder
{
    public class FileCompressor
    {
        public void CompressFromDirectory(string inputDirectory, string outputFileName)
        {
            ZipFile.CreateFromDirectory(inputDirectory, outputFileName);
        }

        public void ExtractToDirectory(string inputFileName, string outputDirectory)
        {
            ZipFile.ExtractToDirectory(inputFileName, outputDirectory);
        }
    }
}
