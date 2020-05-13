using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrueTypeSharp;

namespace TestApplication
{
    public class TextureLabel : MonoGameView
    {
        readonly TrueTypeFont font;

        protected TextureEntity[] textures;

        float scale;

        float fontSize;
        public float FontSize
        {
            get => fontSize;
            set
            {
                fontSize = value;
                scale = ConvertToScale(fontSize);
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

        float ConvertToScale(float pointSize)
        {
            // Convert points to pixels as pointSize * 96 / 72
            float pixelHeight = pointSize * 96.0f / 72.0f;
            return font.GetScaleForPixelHeight(pixelHeight);
        }

        public TextureLabel(string fontName, float fontSize)
        {
            font = new TrueTypeFont(Extensions.GetByteArray(fontName), 0);
            FontSize = fontSize;
        }

        public void Refresh()
        {
            textures = new TextureEntity[Label.Length];
            int startX = Position.X;

            int maxH = 0;

            int minX = 0, maxX = 0;

            for (int j = 0; j < Label.Length; j++)
            {
                var charac = Label[j];
                int width, height, xOffset, yOffset;

                uint index = font.FindGlyphIndex(charac);
                byte[] data = font.GetGlyphBitmap(index, scale, scale, out width, out height, out xOffset, out yOffset);
                var texture = GameContext.CreateTexture2D(data, width, height);

                var textureEntity = textures[j] = new TextureEntity(texture);
                AddChildren(textureEntity);

                if (j == 0)
                    minX = startX;
                else if (j == Label.Length-1)
                    maxX = startX;

                if (charac == ' ')
                {
                    width = 10;
                }

                textureEntity.Allocation = new Rectangle (startX, Position.Y, width, height);

                maxH = Math.Max (maxH, height);

                startX += width + charSeparation;
            }

            Allocation = new Rectangle(Position.X, Position.Y, maxX - minX, maxH);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var texture in textures)
            {
                texture.Draw (spriteBatch, color);
            }
        }
    }
}
