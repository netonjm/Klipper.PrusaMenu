using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public class TextureLine : Texture2D, IDrawableObject
    {
        public Color Color { get; set; } = Color.GreenYellow;
        public int Thrickness { get; set; } = 3;

        public TextureLine(GraphicsDevice graphicsDevice) : base(graphicsDevice, 1, 1, false, SurfaceFormat.Color)
        {
            SetData(new[] { Color.White });
        }

        Vector2 p1;
        public Vector2 P1
        {
            get => p1;
            set => p1 = value;
        }

        Vector2 p2;
        public Vector2 P2
        {
            get => p2;
            set => p2 = value;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(this, P1, P2, Color, Thrickness);
        }

        public void LoadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
