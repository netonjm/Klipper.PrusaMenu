using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TestApplication
{
    public class LoadingScreen : BaseScreen
    {
        DateTime time = DateTime.Now;
        public event EventHandler Finished;
        SpriteBatch spriteBatch;

        public LoadingScreen(GraphicsDeviceManager _graphics) : base(_graphics)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            if (DateTime.Now.Subtract (time).TotalSeconds >= 3)
            {
                Finished?.Invoke(this, EventArgs.Empty);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

           

            spriteBatch.End();
        }
    }
}
