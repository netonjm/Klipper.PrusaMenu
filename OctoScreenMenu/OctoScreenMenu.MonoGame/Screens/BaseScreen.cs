using Microsoft.Xna.Framework;

namespace TestApplication
{
    abstract public class BaseScreen : IMDScreen
    {
        protected GraphicsDeviceManager GraphicsDeviceManager;

        protected Microsoft.Xna.Framework.Graphics.GraphicsDevice GraphicsDevice => GraphicsDeviceManager.GraphicsDevice;

        public BaseScreen(GraphicsDeviceManager _graphics)
        {
            this.GraphicsDeviceManager = _graphics;
        }

        public virtual void Draw(GameTime spriteBatch)
        {

        }

        public virtual void LoadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
