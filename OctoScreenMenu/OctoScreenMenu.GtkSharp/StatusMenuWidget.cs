using System;
using Gdk;

public class StatusMenuWidget : Gtk.VBox, IScreen
{
    public event EventHandler MenuScreen;

    public StatusMenuWidget()
    {
        var fill = new DialogProcessDelegate();
        var location = System.IO.Path.GetDirectoryName(GetType().Assembly.Location);
        fill.Load(this, $"{location}/EmptyXmlFile.xml");
    }

    public void OnKeyDown(EventKey evnt)
    {
        if (evnt.Key == Gdk.Key.Return)
        {
            MenuScreen?.Invoke(this, EventArgs.Empty);
        }
    }

    public void OnRotatoryClicked()
    {
        MenuScreen?.Invoke(this, EventArgs.Empty);
    }
}
