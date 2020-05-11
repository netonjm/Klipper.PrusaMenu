using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace TestApplication
{
    public class VerticalMenu : IDrawableObject
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

        Point position = Point.Zero;
        public Point Position
        {
            get => position;
            set
            {
                position = value;
                Refresh();
            }
        }

        readonly public List<TitleMenu> Items = new List<TitleMenu>();

        GraphicsDeviceManager _graphics;
        public VerticalMenu(GraphicsDeviceManager _graphics)
        {
            this._graphics = _graphics;
        }

        public int SelectedIndex {
            get => Items.IndexOf (selectedItem);
            set
            {
                SelectedItem = Items[value];
            } 
        }

        TitleMenu selectedItem;
        public TitleMenu SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                Refresh();
            }
        }

        public TitleMenu Add (string title)
        {
            var titleMenu = new TitleMenu(_graphics) { Label = title, SelectedColor = Color.Red };
            Items.Add(titleMenu);
            Refresh();
            return titleMenu;
        }

        public void Remove (TitleMenu menu)
        {
            Items.Remove(menu);
            Refresh();
        }

        private void Refresh()
        {
            var it = Position.Y;
            foreach (var item in Items)
            {
                item.Position = new Point(Position.X, it);
                it += item.Height;
                item.IsSelected = selectedItem == item;
            }
        }

        public void LoadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach (var item in Items)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
