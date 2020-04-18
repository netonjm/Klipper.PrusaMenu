using System;
using Gtk;

namespace OctoScreenMenu.IoT
{
    public class MainWindow : Window
    {
        int count = 0;
        readonly Button button;

        public MainWindow() : base(WindowType.Toplevel)
        {
            button = new Button("Click Me");
            button.Clicked += OnBtnActionClicked;
            Add(button);
            ShowAll();
        }

        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
            a.RetVal = true;
        }

        protected void OnBtnActionClicked(object sender, EventArgs e)
        {
            button.Label = string.Format("You pressed {0} times.", ++count);
        }
    }
}