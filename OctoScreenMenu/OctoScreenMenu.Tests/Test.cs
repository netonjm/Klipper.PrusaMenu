using NUnit.Framework;
using System;
using System.Linq;

namespace OctoScreenMenu.Tests
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void TestCase()
        {
            var mainConfigFile = new MainKCfgFile();
            mainConfigFile.Load("/Users/jmedrano/Klipper.PrusaMenu/printer.cfg");
            var mainMenu = mainConfigFile.MainMenuSectionFile;

            var first = mainConfigFile.GetChildren(mainMenu).FirstOrDefault();

            var parent = mainConfigFile.GetParentSectionMenu(first);

            Console.WriteLine("");

        }
    }
}
