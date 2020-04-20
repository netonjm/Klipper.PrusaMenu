using System.Reflection;
using Gdk;
using GLib;
using Gtk;

public interface IScreen
{
    void OnKeyDown(EventKey evnt);
}

enum WindowScreen
{
    Main,
    Menu,
}

public partial class MainWindow : Gtk.Window
{
    IScreen currentWidget;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        ChangeScreen (WindowScreen.Main);

        ShowAll();
    }

    protected override bool OnKeyPressEvent(EventKey evnt)
    {
        currentWidget.OnKeyDown(evnt);
        return base.OnKeyPressEvent(evnt);
    }


    protected override bool OnConfigureEvent(EventConfigure evnt)
    {
        var configure = base.OnConfigureEvent(evnt);
        //RefreshImage ();
        return configure;
    }


    void ChangeScreen (WindowScreen screen)
    {
        switch (screen)
        {
            case WindowScreen.Main:
                var mainScreen = new StatusMenuWidget();
                mainScreen.MenuScreen += (s, e) => {
                    ChangeScreen (WindowScreen.Menu);
                };
                SetScreenWidget (mainScreen);
                break;
            case WindowScreen.Menu:
                var menuScreen = new MenuWidget();
                menuScreen.ShowMainScreen += (s, e) => {
                    ChangeScreen (WindowScreen.Main);
                };
                SetScreenWidget (menuScreen);
                break;
        }
    }

    void SetScreenWidget (IScreen widget)
    {
        if (Child != null)
            Remove(Child);

        currentWidget = widget;
        Add(widget as Widget);
        ShowAll();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}
