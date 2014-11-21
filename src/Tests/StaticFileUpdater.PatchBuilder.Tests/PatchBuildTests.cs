using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using List = NUnit.Framework.List;

namespace StaticFileUpdater.PatchBuilder.Tests
{
    [TestFixture]
    public class PatchBuildTests
    {

        [Test]
        public void GetAllFilesOfDirTest()
        {
            var pbh = new PatchBuildHelper();

            var result = pbh.GetAllFilesOfDirRecursive(Path.Combine(Environment.CurrentDirectory, "TestFiles")).ToList();

            CollectionAssert.Contains(result, Path.Combine(Environment.CurrentDirectory, FileCompressorTests.TestFilesDir, "A.txt"));
            CollectionAssert.Contains(result, Path.Combine(Environment.CurrentDirectory, FileCompressorTests.TestFilesDir, "B.txt"));
            CollectionAssert.Contains(result, Path.Combine(Environment.CurrentDirectory, FileCompressorTests.TestFilesDir, "SubDir", "C.txt"));
            CollectionAssert.Contains(result, Path.Combine(Environment.CurrentDirectory, FileCompressorTests.TestFilesDir, "NotEqual.txt"));
            CollectionAssert.DoesNotContain(result, Path.Combine(Environment.CurrentDirectory, FileCompressorTests.TestFilesDir, "X.txt"));
        }

        private class PatchBuildHelper : PatchBuild
        {
            public PatchBuildHelper(BuildOptions options) : base(options)
            {
            }

            public PatchBuildHelper() : base(new BuildOptions())
            {
            }

            public new IEnumerable<string> GetAllFilesOfDirRecursive(string directory)
            {
                return base.GetAllFilesOfDirRecursive(directory);
            }
        }
    }
}