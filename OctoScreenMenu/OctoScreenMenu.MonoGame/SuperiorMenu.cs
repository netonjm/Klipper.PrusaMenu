﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace TestApplication
{
    public class SuperiorMenu : IDrawableObject
    {
        int y = 30;

        GraphicsDeviceManager graphics;

        TextureLine first;
        TextureLine second;

        TextureLine tab_l, tab_u, tab_r;

        Vector2 lastVector;
        List<TextureLine> lineas = new List<TextureLine>();

        public SuperiorMenu (GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            first = new TextureLine(graphics.GraphicsDevice);
            second = new TextureLine(graphics.GraphicsDevice);

            tab_l = new TextureLine(graphics.GraphicsDevice);
            tab_u = new TextureLine(graphics.GraphicsDevice);
            tab_r = new TextureLine(graphics.GraphicsDevice);

            Refresh();
        }

        public void AddPoint (Vector2 vector)
        {
            var lines = new TextureLine(graphics.GraphicsDevice)
            {
                P1 = lastVector,
                P2 = vector
            };
            lineas.Add(lines);
            lastVector = lines.P2;
            Refresh();
        }

        int tabWidth = 90;

        int tabHeight = 37;

        int tab = 0;
        public int Tab {
            get {
                return tab;
            }
            set {
                tab = value;
                Refresh();
            }
        }

        void Refresh ()
        {
            first.P1 = new Vector2(0, y);
            var pixel = (Tab * tabWidth) + 10;
            first.P2 = new Vector2(pixel, y);

            second.P1 = new Vector2(pixel + tabWidth, y);
            second.P2 = new Vector2(graphics.GraphicsDevice.Viewport.Width, y);

            tab_l.P1 = first.P2;
            tab_l.P2 = new Vector2(pixel, y - tabHeight);

            tab_u.P1 = tab_l.P2;
            tab_u.P2 = new Vector2(pixel + tabWidth, tab_l.P2.Y);

            tab_r.P1 = tab_u.P2;
            tab_r.P2 = new Vector2(pixel + tabWidth, tab_l.P2.Y + tabHeight);
        }

        Point position = Point.Zero;
        public Point Position
        {
            get => position;
            set
            {
                position = value;
                lastVector = new Vector2 (value.X, value.Y);
                Refresh ();
            }
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            first.Draw(spriteBatch);
            second.Draw(spriteBatch);

            tab_l.Draw(spriteBatch);
            tab_u.Draw(spriteBatch);
            tab_r.Draw(spriteBatch);
            //foreach (var item in lineas)
            //{
            //    item.Draw (spriteBatch);
            //}
        }

        public void LoadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
