using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public static class Extensions
    {
        public static void Add (this Vector2 vector, Vector2 vector2)
        {
            Add(vector, vector2.X, vector2.Y);
        }

        public static void Add (this Vector2 vector, float x, float y)
        {
            vector.X += x;
            vector.Y += y;
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, int x, int y, int width, int height, Color color)
        {
            var rect = new Texture2D(spriteBatch.GraphicsDevice, width, height);

            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            rect.SetData(data);

            Vector2 coor = new Vector2(x, y);
            spriteBatch.Draw(rect, coor, Color.White);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color)
        {
            var x = (int)Math.Min(point1.X, point2.X);
            var y = (int)Math.Min(point1.Y, point2.Y);
            var width = (int) Math.Max(point1.X, point2.X) - x;
            var height = (int)Math.Max(point1.Y, point2.Y) - y;
            DrawRectangle(spriteBatch, x, y, width, height, color);
        }

        private static Texture2D CreateTextureLine (SpriteBatch spriteBatch)
        {
               var _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                _texture.SetData(new[] { Color.White });
            return _texture;
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture, Vector2 point1, Vector2 point2, Color color, float thickness = 1f)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine(spriteBatch, texture, point1, distance, angle, color, thickness);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture,  Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(texture, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }

        public static byte[] ToArray(this Stream s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (!s.CanRead)
                throw new ArgumentException("Stream cannot be read");

            MemoryStream ms = s as MemoryStream;
            if (ms != null)
                return ms.ToArray();

            long pos = s.CanSeek ? s.Position : 0L;
            if (pos != 0L)
                s.Seek(0, SeekOrigin.Begin);

            byte[] result = new byte[s.Length];
            s.Read(result, 0, result.Length);
            if (s.CanSeek)
                s.Seek(pos, SeekOrigin.Begin);
            return result;
        }

        public static byte[] ToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] GetByteArray(string name, System.Reflection.Assembly assembly)
        {
            try
            {
                var stream = assembly.GetManifestResourceStream(name);
                return stream.ToArray();
            }
            catch (System.Exception ex)
            {
            }
            return null;
        }

        public static byte[] GetByteArray(string name)
        {
            return GetByteArray(name, typeof(Extensions).Assembly);
        }

        public static Stream GetStream(string name, System.Reflection.Assembly assembly)
        {
            try
            {
                return assembly.GetManifestResourceStream(name);
            }
            catch (System.Exception ex)
            {
            }
            return null;
        }

        public static Stream GetStream(string name)
        {
            return GetStream(name, typeof(Extensions).Assembly);
        }

        public static Texture2D CreateTexture2D(this GraphicsDeviceManager _graphics, string name)
        {
            var stream = GetStream(name);
            var bitmap = new System.Drawing.Bitmap(stream);
            return GetTexture2DFromBitmap(_graphics.GraphicsDevice, bitmap);
        }

        public static Texture2D GetTexture2DFromBitmap(this GraphicsDevice device, System.Drawing.Bitmap bitmap)
        {
            Texture2D tex = new Texture2D(device, bitmap.Width, bitmap.Height);

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bufferSize = data.Height * data.Stride;

            //create data buffer 
            byte[] bytes = new byte[bufferSize];

            // copy bitmap data into buffer
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

            // copy our buffer to the texture
            tex.SetData(bytes);

            // unlock the bitmap data
            bitmap.UnlockBits(data);

            return tex;
        }

        public static Texture2D CreateTexture2D(this GraphicsDeviceManager _graphics, byte[] data, int width, int height)
        {
            Color[] colorData = new Color[data.Length];
            var texture = new Texture2D(_graphics.GraphicsDevice, width, height);
            for (int i = 0; i < colorData.Length; i++)
                colorData[i] = new Color(data[i], data[i], data[i], data[i]);
            texture.SetData(colorData);
            return texture;
        }
    }
}
