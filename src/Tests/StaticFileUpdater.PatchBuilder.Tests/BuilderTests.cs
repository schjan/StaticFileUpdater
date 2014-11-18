using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StaticFileUpdater.PatchBuilder.Tests
{
    [TestFixture]
    public class BuilderTests
    {
        [Test]
        public void ErrorIfNoWorkingDirSet()
        {
            var x = new Builder(new BuildOptions {WorkingDirectory = ""});

            Assert.That(x.Build(), Is.Not.EqualTo(0));
        }
    }
}
