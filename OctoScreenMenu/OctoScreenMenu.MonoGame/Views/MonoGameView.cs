using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public class MonoGameView : IMonoGameObject
    {
        Rectangle allocation = Rectangle.Empty;
        public Rectangle Allocation
        {
            get => allocation;
            set
            {
                allocation = value;
            }
        }

        public Point Position
        {
            get => allocation.Location;
            set
            {
                allocation.Location = value;
                OnNeedsRedraw();
            }
        }

        public Point Size
        {
            get => allocation.Size;
            set
            {
                allocation.Size = value;
                OnNeedsRedraw();
            }
        }

        public int Width
        {
            get => allocation.Width;
            set
            {
                allocation.Width = value;
                OnNeedsRedraw();
            }
        }

        public int Height
        {
            get => allocation.Height;
            set
            {
                allocation.Height = value;
                OnNeedsRedraw();
            }
        }

        protected virtual void OnNeedsRedraw()
        {

        }

        public virtual void LoadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
