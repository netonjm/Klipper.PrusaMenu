using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace TestApplication
{
    public class SuperiorMenuView : MonoGameView
    {
        int y = 30;

        TextureLine first;
        TextureLine second;

        TextureLine tab_l, tab_u, tab_r;

        Vector2 lastVector;
        List<TextureLine> lineas = new List<TextureLine>();

        public SuperiorMenuView()
        {
            first = new TextureLine();
            second = new TextureLine();

            tab_l = new TextureLine();
            tab_u = new TextureLine();
            tab_r = new TextureLine();

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

            second.P2 = new Vector2(GameContext.Width, y);

            tab_l.P1 = first.P2;
            tab_l.P2 = new Vector2(pixel, y - tabHeight);

            tab_u.P1 = tab_l.P2;
            tab_u.P2 = new Vector2(pixel + tabWidth, tab_l.P2.Y);

            tab_r.P1 = tab_u.P2;
            tab_r.P2 = new Vector2(pixel + tabWidth, tab_l.P2.Y + tabHeight);
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
            
            //foreach (var item in lineas)
            //{
            //    item.Draw (spriteBatch);
            //}
        }
    }
}
