using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StaticFileUpdater.BuildTool.Tests
{
    [TestFixture]
    public class ArgsTests
    {
        private const string ExeFilePath = "StaticFileUpdater.BuildTool.exe";

        [Test]
        public void TestHelp()
        {
            using (var x = Process.Start(ExeFilePath, "h"))
            {
                x.WaitForExit();
                Assert.That(x.ExitCode, Is.EqualTo(0));
            }
        }

        [Test, Ignore("Es kan noch nix schiefgehen")]
        public void TestError()
        {
            using (var x = Process.Start(ExeFilePath, "s 4"))
            {
                x.WaitForExit();
                Assert.That(x.ExitCode, Is.Not.EqualTo(0));
            }
        }
    }
}
