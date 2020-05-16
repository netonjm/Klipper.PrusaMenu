using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OctoScreenMenu.MonoGame.Models;
using System;
using System.Collections.Generic;

namespace TestApplication
{
   
    public class LoadingScreen : BaseScreen
    {
        DateTime time = DateTime.Now;
        public event EventHandler Finished;
        SpriteBatch spriteBatch;

        OctoScreenMenu.MonoGame.Sprites.Sprite sprite;
        Animation animation;

        private OctoScreenMenu.MonoGame.Sprites.Sprite _sprite;
        public LoadingScreen() 
        {
            
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GameContext.GraphicsDevice);

            animation = new Animation("WalkUp");
            animation.Frames.Add(new AnimationFrame(GameContext.CreateTexture2D("background.png"), 3));
            animation.Frames.Add(new AnimationFrame(GameContext.CreateTexture2D("background.png"), 5));
            animation.Frames.Add(new AnimationFrame(GameContext.CreateTexture2D("background.png"), 7));
            animation.Frames.Add(new AnimationFrame(GameContext.CreateTexture2D("background.png"), 10));

            sprite = new OctoScreenMenu.MonoGame.Sprites.Sprite(new Animation[] { animation });


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //_sprite.Update(gameTime, _sprites);
            if (DateTime.Now.Subtract (time).TotalSeconds >= 5)
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
