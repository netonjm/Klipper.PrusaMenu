using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public interface IDrawableObject
    {
        void LoadContent();
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
