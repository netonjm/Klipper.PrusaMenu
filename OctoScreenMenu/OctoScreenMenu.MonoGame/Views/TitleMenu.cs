using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public class TitleMenu : TextureLabel
    {
        public bool IsSelected { get; set; }

        public Color SelectedColor { get; set; } = Color.Blue;

        public TitleMenu(GraphicsDeviceManager _graphics) : base("arial.ttf", 40, _graphics)
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
