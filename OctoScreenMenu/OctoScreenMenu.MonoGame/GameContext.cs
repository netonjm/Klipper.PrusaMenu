using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public static class GameContext
    {
        static GameContext()
        {

        }

        public static void Initialize(GraphicsDeviceManager graphicsDeviceManager, KMenuModel menu)
        {
            GraphicsDeviceManager = graphicsDeviceManager;
            Menu = menu;
        }

        public static GraphicsDevice GraphicsDevice => GraphicsDeviceManager.GraphicsDevice;
        public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public static KMenuModel Menu { get; private set; }
        public static int Height => GraphicsDevice.Viewport.Height;
        public static int Width => GraphicsDevice.Viewport.Width;

        public static Texture2D CreateTexture2D(string name)
        {
            return GraphicsDeviceManager.CreateTexture2D(name);
        }

        public static void Clear(Color color)
        {
            GraphicsDevice.Clear(color);
        }

        public static Texture2D CreateTexture2D(byte[] data, int width, int height)
        {
            return GraphicsDeviceManager.CreateTexture2D(data, width, height);
        }
    }
}
