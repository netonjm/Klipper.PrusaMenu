using System;
using System.Collections.Generic;
using System.Reflection;
using Gdk;
using Gtk;
using OctoScreenMenu;

public partial class MainWindow : Gtk.Window
{
    Gtk.Image backgroundImageView;
    readonly string imagePath;
    readonly DrawingArea darea;

    readonly Fixed absoluteContainer;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        absoluteContainer = new Fixed();
        Add(absoluteContainer);

        var directoryImagePath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        imagePath = System.IO.Path.Combine(directoryImagePath, "CentreLIT.png");

        var pixBuf = new Pixbuf(imagePath, 500, 500, false);
        backgroundImageView = new Gtk.Image(pixBuf);

        absoluteContainer.Put(backgroundImageView, 0, 0);

        var redArrowLeft = System.IO.Path.Combine(directoryImagePath, "red-arrow-left-icon-225849.png");
        var redArrowLeftImage = new Gtk.Image(redArrowLeft);

        var currentButton = new Button();
        currentButton.Label = "dsadsdsasdsda";
        currentButton.WidthRequest = 100;
        currentButton.HeightRequest = 100;
        currentButton.Relief = ReliefStyle.Half;
        absoluteContainer.Put(currentButton, 30, 30);

        var button2 = new Button();
        button2.WidthRequest = 100;
        button2.HeightRequest = 100;
        button2.Relief = ReliefStyle.Half;
        absoluteContainer.Put(button2, 60, 60);

        ShowAll();
    }

    //private void BackgraundDrawingAreaExposeEvent(object o, ExposeEventArgs args)
    //{
    //    var area = (DrawingArea)o;
    //    Cairo.Context cr = Gdk.CairoHelper.Create(area.GdkWindow);
    //    var imageSurface = new Cairo.ImageSurface(imagePath);
    //    float w = imageSurface.Width;
    //    float h = imageSurface.Height;
    //    cr.Scale(Allocation.Width / w, Allocation.Height / h);
    //    CairoHelper.SetSourcePixbuf(cr, pi, 0, 0);
    //    cr.Paint();
    //    cr.Fill();
    //    ((IDisposable)cr.Target).Dispose();
    //    ((IDisposable)cr).Dispose();
    //}

    void RefreshImage ()
    {
        //darea.WidthRequest = Allocation.Width;
        //darea.HeightRequest = Allocation.Height;
    }

    protected override bool OnConfigureEvent(EventConfigure evnt)
    {
        var configure = base.OnConfigureEvent(evnt);

        RefreshImage ();

        return configure;
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}
