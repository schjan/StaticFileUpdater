using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StaticFileUpdater.PatchBuilder.Tests
{
    [TestFixture]
    public class FileComparerTests
    {
        [Test]
        public void Equal()
        {
            var result = FileComparer.FilesAreEqual("TestFiles/A.txt", "TestFiles/B.txt");

            Assert.True(result);
        }

		[Test]
		public async Task EqualAsync()
		{
			var result = await FileComparer.FilesAreEqualAsync("TestFiles/A.txt", "TestFiles/B.txt");

			Assert.True(result);
		}

        [Test]
        public void UnequalA()
        {
            var result = FileComparer.FilesAreEqual("TestFiles/A.txt", "TestFiles/NotEqual.txt");

            Assert.False(result);
        }

        [Test]
        public void UnequalB()
        {
            var result = FileComparer.FilesAreEqual("TestFiles/NotEqual.txt", "TestFiles/A.txt");

            Assert.False(result);
        }

        [Test, ExpectedException(typeof(FileNotFoundException))]
        public void NoFileA()
        {
            FileComparer.FilesAreEqual("TestFiles/C.txt", "TestFiles/A.txt");
        }

        [Test, ExpectedException(typeof(FileNotFoundException))]
        public void NoFileB()
        {
            FileComparer.FilesAreEqual("TestFiles/A.txt", "TestFiles/C.txt");
        }
    }
}
