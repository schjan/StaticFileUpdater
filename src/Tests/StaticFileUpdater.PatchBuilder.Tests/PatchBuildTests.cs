using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NUnit.Framework;
using StaticFileUpdater.Common;
using List = NUnit.Framework.List;

namespace StaticFileUpdater.PatchBuilder.Tests
{
    [TestFixture]
    public class PatchBuildTests
    {
        private static string TestFilesDir
        {
            get { return Path.Combine(Environment.CurrentDirectory, FileCompressorTests.TestFilesDir); }
        }

        [Test]
        public void GetAllFilesOfDirTest()
        {
            var pbh = new PatchBuildHelper();

            var result = pbh.GetAllFilesOfDirRecursive(TestFilesDir).ToList();

            CollectionAssert.Contains(result, "A.txt");
            CollectionAssert.Contains(result, "B.txt");
            CollectionAssert.Contains(result, Path.Combine("SubDir", "C.txt"));
            CollectionAssert.Contains(result, "NotEqual.txt");
            CollectionAssert.DoesNotContain(result, "X.txt");
        }


        [Test]
        public void CheckFilesOfSameDirIsNull()
        {
            var pbh = new PatchBuildHelper(new BuildOptions
            {
                LastPatchDirectory = TestFilesDir,
                WorkingDirectory = TestFilesDir
            });

            var result = pbh.BuildPatchLocal();

            Assert.That(result.Added.Count, Is.EqualTo(0));
            Assert.That(result.Updated.Count, Is.EqualTo(0));
            Assert.That(result.Deleted.Count, Is.EqualTo(0));
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

            public new PatchFiles CheckFiles()
            {
                return base.CheckFiles();
            }
        }
    }
}