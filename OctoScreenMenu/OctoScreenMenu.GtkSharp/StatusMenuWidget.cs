using System;
using Gdk;

public class StatusMenuWidget : Gtk.VBox, IScreen
{
    public event EventHandler MenuScreen;

    public StatusMenuWidget()
    {
        var fill = new DialogProcessDelegate();
        fill.Load(this, "/Users/jmedrano/Klipper.PrusaMenu/OctoScreenMenu/OctoScreenMenu.GtkSharp/EmptyXmlFile.xml");
    }

    public void OnKeyDown(EventKey evnt)
    {
        if (evnt.Key == Gdk.Key.Return)
        {
            MenuScreen?.Invoke(this, EventArgs.Empty);
        }
    }
}
