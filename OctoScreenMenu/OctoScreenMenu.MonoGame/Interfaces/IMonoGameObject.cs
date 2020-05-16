using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TestApplication
{
    public interface IMDScreen
    {
        void LoadContent();
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }

    public interface IMonoGameObject
    {
        Point Position { get; set; }
        void LoadContent();
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
