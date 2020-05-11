using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public class TextureEntity
    {
        public TextureEntity (Texture2D texture, Point position)
        {
            Texture = texture;
            Position = position;
        }

        public Texture2D Texture { get; private set; }

        Point position;
        public Point Position
        {
            get => position;
            set => position = value;
        }

        public int Width => Texture.Width;
        public int Height => Texture.Height;

        internal void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Texture, new Rectangle(Position.X, Position.Y, Width, Height), color);
        }
    }
}
