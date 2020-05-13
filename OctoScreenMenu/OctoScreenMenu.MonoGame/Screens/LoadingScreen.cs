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

        public LoadingScreen() 
        {
            
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GameContext.GraphicsDevice);

            base.LoadContent();
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
            GameContext.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
        

            spriteBatch.End();
        }
    }
}
