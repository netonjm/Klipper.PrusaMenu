using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public class TextureEntity : MonoGameView
    {
        public TextureEntity (Texture2D texture)
        {
            Texture = texture;
        }

        public Texture2D Texture { get; private set; }

        internal void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Texture, AbsoluteBounds, color);
        }
    }
}
