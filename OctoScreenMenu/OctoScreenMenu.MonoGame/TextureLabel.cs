using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrueTypeSharp;

namespace TestApplication
{
    public class TextureLabel : IDrawableObject
    {
        readonly TrueTypeFont font;

        GraphicsDeviceManager _graphics;
        protected TextureEntity[] textures;

        float scale;
        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                Refresh();
            }
        }

        int charSeparation = 3;
        public int CharSeparation
        {
            get => charSeparation;
            set
            {
                charSeparation = value;
                Refresh();
            }
        }

        protected Color color = Color.Black;
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                Refresh();
            }
        }


        string label = string.Empty;
        public string Label
        {
            get => label;
            set
            {
                label = value;
                Refresh();
            }
        }

        int width;
        public int Width
        {
            get => width;
            set
            {
                width = value;
                Refresh();
            }
        }

        int height;
        public int Height
        {
            get => height;
            set
            {
                height = value;
                Refresh();
            }
        }

        float ConvertToScale(float pointSize)
        {
            // Convert points to pixels as pointSize * 96 / 72
            float pixelHeight = pointSize * 96.0f / 72.0f;
            return font.GetScaleForPixelHeight(pixelHeight);
        }

        public TextureLabel(string fontName, float fontSize, GraphicsDeviceManager _graphics)
        {
            this._graphics = _graphics;
            font = new TrueTypeFont(Helpers.GetByteArray(fontName), 0);
            Scale = ConvertToScale(fontSize);
        }

        public void Refresh()
        {
            textures = new TextureEntity[Label.Length];
            int startX = position.X;
            for (int j = 0; j < Label.Length; j++)
            {
                int width, height, xOffset, yOffset;
                uint index = font.FindGlyphIndex(Label[j]);
                byte[] data = font.GetGlyphBitmap(index, Scale, Scale, out width, out height, out xOffset, out yOffset);
                var texture = _graphics.CreateTexture2D(data, width, height);
                textures[j] = new TextureEntity (texture, new Point (startX, position.Y));

                startX += width + charSeparation;

                this.width = Math.Max(height, startX - position.X);
                this.height = Math.Max(height, this.height);
            }
        }

        Point position = Point.Zero;
        public Point Position
        {
            get => position;
            set
            {
                position = value;
                Refresh();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var texture in textures)
            {
                texture.Draw (spriteBatch, color);
            }
        }

        public void LoadContent()
        {
           
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
