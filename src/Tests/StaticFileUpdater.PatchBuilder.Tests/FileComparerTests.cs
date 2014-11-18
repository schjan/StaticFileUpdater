using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StaticFileUpdater.PatchBuilder.Tests
{
    [TestFixture]
    public class FileComparerTests
    {
        FileComparer fileComparer;

        [SetUp]
        public void SetUp()
        {
            fileComparer = new FileComparer();
        }

        [Test]
        public void Equal()
        {
            var result = fileComparer.FilesAreEqual("TestFiles/A.txt", "TestFiles/B.txt");

            Assert.True(result);
        }

		[Test]
		public async Task EqualAsync()
		{
			var result = await fileComparer.FilesAreEqualAsync("TestFiles/A.txt", "TestFiles/B.txt");

			Assert.True(result);
		}

        [Test]
        public void UnequalA()
        {
            var result = fileComparer.FilesAreEqual("TestFiles/A.txt", "TestFiles/NotEqual.txt");

            Assert.False(result);
        }

        [Test]
        public async Task UnequalAAsync()
        {
            var result = await fileComparer.FilesAreEqualAsync("TestFiles/A.txt", "TestFiles/NotEqual.txt");

            Assert.False(result);
        }

        [Test]
        public void UnequalB()
        {
            var result = fileComparer.FilesAreEqual("TestFiles/NotEqual.txt", "TestFiles/A.txt");

            Assert.False(result);
        }

        [Test, ExpectedException(typeof(FileNotFoundException))]
        public void NoFileA()
        {
            fileComparer.FilesAreEqual("TestFiles/C.txt", "TestFiles/A.txt");
        }

        [Test, ExpectedException(typeof(FileNotFoundException))]
        public void NoFileB()
        {
            fileComparer.FilesAreEqual("TestFiles/A.txt", "TestFiles/C.txt");
        }

        [Test, ExpectedException(typeof(FileNotFoundException))]
        public async Task NoFileAAsync()
        {
            await fileComparer.FilesAreEqualAsync("TestFiles/C.txt", "TestFiles/A.txt");
        }
    }
}
