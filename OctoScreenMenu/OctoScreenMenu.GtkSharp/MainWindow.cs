using System;
using System.Collections.Generic;
using System.Reflection;
using Gtk;
using OctoScreenMenu;

public partial class MainWindow : Gtk.Window
{

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        var imagePath = System.IO.Path.Combine(path, "CentreLIT.png");


        var h = new Fixed();

        var btn = new Button();
        btn.Relief = ReliefStyle.Half;
        btn.BorderWidth = 0;
        Gtk.Image dd = new Image(imagePath);
        btn.Image = dd;
        btn.ModifyBg(StateType.Selected, new Gdk.Color (0,0,0));
        h.Put(btn, 20, 20);
        this.Add(h);
        ShowAll();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}
