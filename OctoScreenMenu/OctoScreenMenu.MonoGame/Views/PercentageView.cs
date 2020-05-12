using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public class PercentageView : MonoGameView
    {
        public Color Color { get; set; } = Color.Aqua;

        public int Value { get; set; }
        public int MaxValue { get; set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(0, 0, 100, 100, Color);
        }

        protected override void OnNeedsRedraw()
        {

        }
    }
}
