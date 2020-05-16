using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;
namespace TestApplication
{
    public class SuperiorMenuView : MonoGameView
    {
        public event EventHandler SelectedIndexChanged;

        int y = 30;

        TextureLine first;
        TextureLine second;

        TextureLine tab_l, tab_u, tab_r;

        Vector2 lastVector;

        int offsetFirstItem = 90;
        public int OffsetFirstItem
        {
            get => offsetFirstItem;
            set
            {
                offsetFirstItem = value;
                Refresh();
            }
        }

        List<TextureLine> lineas = new List<TextureLine>();

        public SuperiorMenuView()
        {
            first = new TextureLine();
            AddChildren(first);

            second = new TextureLine();
            AddChildren(second);

            tab_l = new TextureLine();
            AddChildren(tab_l);
            tab_u = new TextureLine();
            AddChildren(tab_u);
            tab_r = new TextureLine();
            AddChildren(tab_r);

            Refresh();
        }

        public void AddPoint (Vector2 vector)
        {
            var lines = new TextureLine()
            {
                P1 = lastVector,
                P2 = vector
            };
            lineas.Add(lines);
            lastVector = lines.P2;
            Refresh();
        }

        int itemHeight = 37;
        public int ItemHeight {
            get => itemHeight;
            set
            {
                itemHeight = value;
                Refresh();
            }
        }

        int itemWidth = 90;
        public int ItemWidth
        {
            get => itemWidth;
            set
            {
                itemWidth = value;
                Refresh();
            }
        }

        int selectedIndex = 0;
        public int SelectedIndex {
            get {
                return selectedIndex;
            }
            set {
                selectedIndex = value;
                Refresh();
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        void Refresh ()
        {
            first.P1 = new Vector2(0, y);
            var pixel = (SelectedIndex * ItemWidth) + OffsetFirstItem;

            first.P2 = new Vector2(pixel, y);

            second.P1 = new Vector2(pixel + ItemWidth, y);

            second.P2 = new Vector2(GameContext.Width, y);

            tab_l.P1 = first.P2;
            tab_l.P2 = new Vector2(pixel, y - ItemHeight);

            tab_u.P1 = tab_l.P2;
            tab_u.P2 = new Vector2(pixel + ItemWidth, tab_l.P2.Y);

            tab_r.P1 = tab_u.P2;
            tab_r.P2 = new Vector2(pixel + ItemWidth, tab_l.P2.Y + ItemHeight);
        }

        protected override void OnNeedsRedraw()
        {
            lastVector = new Vector2(Position.X, Position.Y);
        }

        public override void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            first.Draw(gameTime,spriteBatch);
            second.Draw(gameTime, spriteBatch);

            tab_l.Draw(gameTime, spriteBatch);
            tab_u.Draw(gameTime, spriteBatch);
            tab_r.Draw(gameTime, spriteBatch);
        }
    }
}
