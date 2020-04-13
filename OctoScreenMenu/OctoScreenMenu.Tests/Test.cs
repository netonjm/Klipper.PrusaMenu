using NUnit.Framework;
using System;
namespace OctoScreenMenu.Tests
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void TestCase()
        {
            KCfgFile kCfgFile = new KCfgFile();
            kCfgFile.Load("/Users/jmedrano/Klipper.PrusaMenu/printer.cfg");

        }
    }
}
