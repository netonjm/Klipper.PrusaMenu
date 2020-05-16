using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public class TextureLine : MonoGameView
    {
        Texture2D texture2D;

        public Color Color { get; set; } = Color.GreenYellow;
        public int Thrickness { get; set; } = 3;

        public TextureLine()
        {
            texture2D = new Texture2D(GameContext.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture2D.SetData(new[] { Color.White });
        }

        Vector2 p1;
        public Vector2 P1
        {
            get => p1;
            set => p1 = value;
        }

        Vector2 p2;
        public Vector2 P2
        {
            get => p2;
            set => p2 = value;
        }

        public Vector2 AbsoluteP1
        {
            get
            {
                return new Vector2 (
                    Parent.AbsoluteBounds.X + p1.X,
                     Parent.AbsoluteBounds.Y + p1.Y
                    );
            }
        }

        public Vector2 AbsoluteP2
        {
            get
            {
                return new Vector2(
                    Parent.AbsoluteBounds.X + p2.X,
                     Parent.AbsoluteBounds.Y + p2.Y
                    );
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(texture2D, AbsoluteP1, AbsoluteP2, Color, Thrickness);
        }
    }
}
