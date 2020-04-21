using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using OctoScreenMenu;
using Gdk;

class MenuWidget : Gtk.HBox, IScreen
{
    Gtk.ListStore menuItemsStore;
    Gtk.TreeView menuTreeView;

    Dictionary<TreeIter, MenuSectionFile> cacheData = new Dictionary<TreeIter, MenuSectionFile>();

    public event System.EventHandler ShowMainScreen;

   
    MenuSectionFile mainMenuConfig;
    MainKCfgFile configFile;

    MenuSectionFile actualMenuConfig;
    MenuSectionFile[] menuChildrenItems;
    MenuSectionFile parentMenuConfig;

    public MenuWidget(IntPtr raw) : base(raw)
    {
    }

    public MenuWidget()
    {
        Initialzee();
    }

    void Initialzee()
    {
        configFile = new MainKCfgFile();

        string homePath = Environment.OSVersion.Platform == PlatformID.Unix ? "/home/pi" : 
                   Environment.OSVersion.Platform == PlatformID.MacOSX
    ? Environment.GetEnvironmentVariable("HOME")
    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

        string filePath = $"{homePath}/printer.cfg";
        configFile.Load(filePath);

        mainMenuConfig = configFile.MainMenuSectionFile;
        SetMenuItem(mainMenuConfig);

        // Create our TreeView
        menuTreeView = new Gtk.TreeView();
        menuTreeView.HeadersVisible = false;
        menuTreeView.RowActivated += MenuTreeView_RowActivated;
        // Add our tree to the window
        PackStart (menuTreeView, true, true, 0);

      
        // Create a column for the artist name
        var menuColumn = new Gtk.TreeViewColumn();
        menuColumn.Title = "Menu";
        // Create a column for the song title
        //Gtk.TreeViewColumn songColumn = new Gtk.TreeViewColumn();
        //songColumn.Title = "Song Title";

        var renderer = new DeviceAgentCellRenderer();
        // Add the columns to the TreeView
        menuTreeView.AppendColumn("", renderer,(CellLayoutDataFunc) DataFunc);

        //tree.AppendColumn(songColumn);

        // Create a model that will hold two strings - Artist Name and Song Title
        menuItemsStore = new Gtk.ListStore(typeof(string), typeof(MenuSectionFile));
        menuTreeView.Model = menuItemsStore;

        Gtk.CellRendererText artistNameCell = new Gtk.CellRendererText();

        menuColumn.PackStart(artistNameCell, true);
        menuColumn.AddAttribute(artistNameCell, "text", 0);

        Refresh(null);
    }

    static void DataFunc (ICellLayout cell_layout, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
    {
        var server = (MenuSectionFile)tree_model.GetValue(iter, 1);
        if (cell is DeviceAgentCellRenderer renderer)
        {
            renderer.SetData(server);
        }
    }

    void SetMenuItem(MenuSectionFile menuSection)
    {
        actualMenuConfig = menuSection;
        parentMenuConfig = configFile.GetParentSectionMenu(menuSection);
        menuChildrenItems = configFile.GetChildren(menuSection)
            .ToArray();
    }

    void Refresh(MenuSectionFile parent)
    {
        cacheData.Clear();
        menuItemsStore.Clear();

        if (actualMenuConfig != mainMenuConfig)
            cacheData.Add(menuItemsStore.AppendValues("Parent...", parent), parent);
        else
            cacheData.Add(menuItemsStore.AppendValues("Main", null), null);

        foreach (var item in menuChildrenItems)
        {
            if (string.IsNullOrEmpty(item.RespondAction))
            {
                var iter = menuItemsStore.AppendValues(item.Title, item);
                cacheData.Add(iter, item);
            }
        }
    }


    private void MenuTreeView_RowActivated(object o, RowActivatedArgs args)
    {
        if (menuItemsStore.GetIter(out TreeIter iter, args.Path))
        {
            if (menuItemsStore.GetValue(iter, 1) is MenuSectionFile selectedValue)
            {
                if (selectedValue.Action == "exit")
                {
                    ShowMainScreen?.Invoke(this, EventArgs.Empty);
                }
                else if (selectedValue.Items.Count > 0)
                {
                    var parent = actualMenuConfig;
                    SetMenuItem(selectedValue);
                    Refresh(parent);
                }
            } else
            {
                ShowMainScreen?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    protected override bool OnConfigureEvent(EventConfigure evnt)
    {
        menuTreeView.WidthRequest = Allocation.Width;
        menuTreeView.HeightRequest = Allocation.Height; ;
        return base.OnConfigureEvent(evnt);
    }

    private void CurrentButton_Activated(object sender, EventArgs e)
    {
        menuItemsStore.Clear();
        menuItemsStore.AppendValues("Garbawwwwwwwge");
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

    void RefreshImage()
    {
        //darea.WidthRequest = Allocation.Width;
        //darea.HeightRequest = Allocation.Height;
    }

    public void OnKeyDown (EventKey evnt)
    {
        //ShowMainScreen?.Invoke(this, EventArgs.Empty);
    }

    public void OnRotatoryClicked()
    {
        ShowMainScreen?.Invoke(this, EventArgs.Empty);
    }
}
