using System;
using System.Xml.Linq;
using Gtk;
using System.Linq;

public class DialogProcessDelegate
{
    public void Load(Box box, string file)
    {
        XDocument doc = XDocument.Load(file);
        foreach (XElement element in doc.Root.Elements())
        {
            AppendElement(box, element);
            Console.WriteLine(element);
        }
    }

    void AppendElement(Widget content, XElement main)
    {
        var names = $"Gtk.{main.Name.LocalName}";
        var type = typeof(Gtk.Widget).Assembly.GetTypes()
            .FirstOrDefault(s => s.FullName == names);

        Gtk.Widget obj = null;

        if (main.Name.LocalName != "Image")
        {
            obj = (Gtk.Widget)Activator.CreateInstance(type, new object[0]);
        }
        else
        {
            obj = new Gtk.Image("/Users/jmedrano/Klipper.PrusaMenu/OctoScreenMenu/OctoScreenMenu.GtkSharp/LeftLIT.png");
        }

        if (content is Box box)
        {
            int padding = 0;
            int.TryParse(main.Attribute("padding")?.Value, out padding);

            bool expand = false;
            bool.TryParse(main.Attribute("expand")?.Value, out expand);

            bool fill = false;
            bool.TryParse(main.Attribute("fill")?.Value, out fill);

            box.PackStart(obj, expand, fill, (uint)padding);
        }

        if (obj is Gtk.Label lbl)
        {
           
            var font = lbl.Style.FontDescription.Copy();

            if (font.SizeIsAbsolute)
            {
                font.AbsoluteSize = font.Size + 1;
            }
            else
            {
                font.Size += (int)(Pango.Scale.PangoScale);
            }
            lbl.ModifyFont(font);
            lbl.Text = main.Attribute("text")?.Value ?? "";

        }
        else if (obj is Gtk.Box cbox)
        {
            int space = 0;
            int.TryParse(main.Attribute("space")?.Value, out space);
            cbox.Spacing = space;
        }

        if (int.TryParse(main.Attribute("width")?.Value, out int result))
        {
            obj.WidthRequest = result;
        }
        if (int.TryParse(main.Attribute("height")?.Value, out int result2))
        {
            obj.HeightRequest = result2;
        }

        foreach (XElement element in main.Elements())
        {
            AppendElement(obj, element);
        }
    }
}
