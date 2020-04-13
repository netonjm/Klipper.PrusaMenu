using System;
using System.Collections.Generic;
using Gtk;

namespace OctoScreenMenu
{

    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();
        }
    }
}
