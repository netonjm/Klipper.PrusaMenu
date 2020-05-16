using System;
using Gtk;

namespace OctoScreenMenu.IoT
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow() { WidthRequest = 300, HeightRequest = 200 };
            win.Show();
            Application.Run();
        }
    }
}
