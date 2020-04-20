using Gtk;
using OctoScreenMenu;

class DeviceAgentCellRenderer : CellRenderer
{
    const int horizontal = 10;
    const int fontSeparation = 20;
    readonly int imageHeight;
    int icon = 30;

    MenuSectionFile server { get; set; }

    public DeviceAgentCellRenderer()
    {
        Mode = CellRendererMode.Inert;
        imageHeight = 20;
    }

    public void SetData(MenuSectionFile server)
    {
        this.server = server;
    }

    static StateType GetState(Widget widget, CellRendererState flags)
    {
        if (flags.HasFlag(CellRendererState.Selected))
        {
            if (widget.IsFocus)
            {
                return StateType.Selected;
            }

            return StateType.Active;
        }

        return StateType.Normal;
    }

    public override void GetSize(Widget widget, ref Gdk.Rectangle cell_area, out int x_offset, out int y_offset, out int width, out int height)
    {
        const int spacing = 5;
        x_offset = 0;
        y_offset = 0;

        var visibleRect = widget.Allocation;
        width = visibleRect.Width - 10;
        height = (int)icon + spacing * 2;
    }

    protected override void Render(Gdk.Drawable window, Widget widget, Gdk.Rectangle background_area, Gdk.Rectangle cell_area, Gdk.Rectangle expose_area, CellRendererState flags)
    {
        if (server != null && !Visible)
            return;

        int leftMargin = cell_area.X + horizontal - 1; // VV: Compensate the icon negative margin
        int topMargin = cell_area.Y + 4; // VV: Compensate the icon negative margin


        int right = cell_area.Right;

        var fontColumnX = imageHeight + leftMargin + horizontal;
        var fontColumnY = topMargin + horizontal;

        var totalWidth = widget.Allocation.Width;

        using (var layout = new Pango.Layout(widget.PangoContext))
        {
            layout.Alignment = Pango.Alignment.Left;

            using (var cr = Gdk.CairoHelper.Create(window))
            {
                //Xwt.Drawing.Image actualIcon = null;
                //if (server.IsKnown)
                //{
                //    actualIcon = server.IsConnected ? IoTConnectedIcon : IoTDisconnectedIcon;
                //    isLinked = serverSelected != null && serverSelected.IpAddress.ToString() == server.IpAddress.ToString();
                //}
                //else
                //{
                //    actualIcon = IoTDiscoveredIcon;
                //}
                //cr.DrawImage(widget, actualIcon, leftMargin, topMargin);

                if (server != null && server.Items.Count > 0 && server.Title != "Main Menu")
                {
                    var isSelected = flags.HasFlag(CellRendererState.Selected);

                    if (isSelected)
                    {
                        DrawDownArrow(cr, right, topMargin);
                        cr.SetSourceRGB(255, 255, 255);
                    }
                    else
                    {
                        DrawRightArrow(cr, right, topMargin);

                       
                        cr.SetSourceRGB(0, 0, 0);
                    }
                    cr.Fill();
                }

                StateType state = GetState(widget, flags);

                layout.SetMarkup($"<b>{server?.Title ?? "Status"}</b>");
                window.DrawLayout(widget.Style.TextGC(state), fontColumnX, fontColumnY, layout);

                //TODO: hack! we need a better way from the model
                //string subtitle = server.GetMacAddress();
                //if (server.IsKnown)
                //{
                //    subtitle = server.Fingerprint;
                //}
                //layout.SetMarkup(subtitle);
                //window.DrawLayout(widget.Style.TextGC(state), fontColumnX, fontColumnY + fontSeparation, layout);
            }


        }
    }

    public void DrawRightArrow(Cairo.Context cr, int right, int topMargin)
    {
        var middle = right - 28;
        var topArrow = topMargin + 12;
        cr.MoveTo(middle, topArrow);

        topArrow += 10;
        cr.LineTo(new Cairo.PointD(middle, topArrow));
        middle += 6;
        topArrow -= 5;
        cr.LineTo(new Cairo.PointD(middle, topArrow));
        middle -= 6;
        topArrow -= 5;
        cr.LineTo(new Cairo.PointD(middle, topArrow));
    }

    public void DrawDownArrow(Cairo.Context cr, int right, int topMargin)
    {
        var middle = right - 30;
        var topArrow = topMargin + 14;
        cr.MoveTo(middle, topArrow);


        middle += 10;
        cr.LineTo(new Cairo.PointD(middle, topArrow));
        middle -= 5;
        topArrow += 6;
        cr.LineTo(new Cairo.PointD(middle, topArrow));
        middle -= 5;
        topArrow -= 6;
        cr.LineTo(new Cairo.PointD(middle, topArrow));
    }
}
