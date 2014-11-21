using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StaticFileUpdater.PatchBuilder.Tests
{
    [TestFixture]
    public class FileCompressorTests
    {
        public const string TestDir = "CompressTest";
        public const string TestZip = "CompressTest.zip";
        public const string TestFilesDir = "TestFiles";
        private FileCompressor fileCompressor;
        
        [SetUp]
        public void SetUp()
        {
            DeleteFilesIfExist();
            fileCompressor = new FileCompressor();
        }

        [Test]
        public void CompressAndUncompress()
        {
            fileCompressor.CompressFromDirectory(TestFilesDir, TestZip);

            Assert.True(File.Exists(TestZip));

            fileCompressor.ExtractToDirectory(TestZip, TestDir);

            Assert.True(Directory.Exists(TestDir));
            Assert.True(File.Exists(TestDir + "/A.txt"));
            Assert.True(File.Exists(TestDir + "/B.txt"));
            Assert.True(File.Exists(TestDir + "/NotEqual.txt"));
        }

        [TearDown]
        public void TearDown()
        {
            DeleteFilesIfExist();
        }

        private void DeleteFilesIfExist()
        {
            if (Directory.Exists(TestDir))
                Directory.Delete(TestDir, true);

            if (File.Exists(TestZip))
                File.Delete(TestZip);
        }
    }
}
