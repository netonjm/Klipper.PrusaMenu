using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TestApplication
{
    abstract public class BaseScreen : IMDScreen
    {
        readonly public List<MonoGameView> Children = new List<MonoGameView>(); 

        public BaseScreen()
        {

        }

        public virtual void Draw(GameTime spriteBatch)
        {

        }

        public virtual void LoadContent()
        {
            foreach (var item in Children)
                item.LoadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var item in Children)
                item.Update(gameTime);
        }

        public virtual void Initialize()
        {
            foreach (var item in Children)
                item.Initialize ();
        }
    }
}
