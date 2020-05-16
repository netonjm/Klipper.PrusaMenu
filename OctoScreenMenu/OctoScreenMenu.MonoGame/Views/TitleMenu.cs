using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public class TitleMenu : TextureLabel
    {
        public bool IsSelected { get; set; }

        public Color SelectedColor { get; set; } = Color.Blue;

        public TitleMenu() : base("arial.ttf", 40)
        {
            Color = Color.GreenYellow;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var texture in textures)
            {
                texture.Draw(spriteBatch, IsSelected ? SelectedColor : color);
            }
        }
    }
}
