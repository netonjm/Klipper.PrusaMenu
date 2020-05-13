using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace TestApplication
{
    public class MenuListView : MonoGameView
    {
        int separation = 3;
        public int Separation
        {
            get => separation;
            set
            {
                separation = value;
                Refresh();
            }
        }

        int fontSize = 20;
        public int FontSize
        {
            get => fontSize;
            set
            {
                fontSize = value;
                Refresh();
            }
        }

        readonly List<(TitleMenu, object)> data = new List<(TitleMenu, object)>();
        public IReadOnlyList<TitleMenu> Items => data.Select(s => s.Item1)
            .ToList()
            .AsReadOnly();

        public MenuListView()
        {
        }

        int IndexOf(TitleMenu titleMenu)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Item1 == titleMenu)
                    return i;
            }
            return -1;
        }

        public int SelectedIndex {
            get => IndexOf (SelectedTitle);
            set
            {
                if (value == -1)
                    SelectedTitle = null;
                SelectedTitle = Items[value];
            } 
        }

        (TitleMenu, object) selectedItem;
        public TitleMenu SelectedTitle
        {
            get => selectedItem.Item1;
            set
            {
                if (value != null)
                {
                    var first = data.FirstOrDefault(s => s.Item1 == value);
                    selectedItem = first;
                } else
                {
                    selectedItem = default;
                }
              
                Refresh();
            }
        }

        public object SelectedItem
        {
            get => selectedItem.Item2;
            set
            {
                var first = data.FirstOrDefault(s => s.Item2 == value);
                selectedItem = first;
                Refresh();
            }
        }

        public void Clear(bool refresh = true)
        {
            var items = Items.ToArray ();
            foreach (var item in items)
                Remove(item, false);

            if (refresh)
                Refresh();
        }

        public TitleMenu Add (string title, object element, bool refresh = true)
        {
            var titleMenu = new TitleMenu() {
                Label = title,
                SelectedColor = Color.Red
            };
            data.Add((titleMenu, element));
            AddChildren(titleMenu);
            if (refresh)
            Refresh();
            return titleMenu;
        }

        public void Remove (TitleMenu menu, bool refresh = true)
        {
            RemoveChildren(menu);

            var first = data.FirstOrDefault(s => s.Item1 == menu);
            data.Remove(first);

            if (refresh)
                Refresh();
        }

        public void Refresh()
        {
            var it = Position.Y;
            foreach (var item in data)
            {
                item.Item1.Position = new Point(Position.X, it);
                it += item.Item1.Height + separation;
                item.Item1.IsSelected = selectedItem == item;
                item.Item1.FontSize = fontSize;
            }
        }

        public override void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var item in Items)
            {
                item.Draw (gameTime, spriteBatch);
            }
        }
    }
}
