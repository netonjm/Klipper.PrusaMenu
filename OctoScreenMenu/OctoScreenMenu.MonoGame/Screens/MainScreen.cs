using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestApplication
{
    public class MainScreen : BaseScreen
    {
        SpriteBatch spriteBatch;
        VerticalMenu mainMenu;
        Texture2D background;
        SuperiorMenuView superiorMenu;

        KeyboardState parentState;

        public MainScreen(GraphicsDeviceManager _graphics) : base(_graphics)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainMenu = new VerticalMenu(GraphicsDeviceManager);
            mainMenu.Position = new Point(100, 200);
            mainMenu.Separation = 10;
            mainMenu.Add("HOLA");
            var menu = mainMenu.Add("MENU");
            mainMenu.Add("MI CASA");
            mainMenu.Add("TEST");
            mainMenu.Add("LOL");

            superiorMenu = new SuperiorMenuView(GraphicsDeviceManager);
            superiorMenu.Position = new Point(0, 0);

            background = GraphicsDeviceManager.CreateTexture2D("background.png");
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDeviceManager.GraphicsDevice.Viewport.Width, GraphicsDeviceManager.GraphicsDevice.Viewport.Height), Color.White);
            superiorMenu.Draw(gameTime, spriteBatch);
            mainMenu.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void LoadContent()
        {
           
        }

        public override void Update(GameTime gameTime)
        {
            // Poll for current keyboard state
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyUp(Keys.Down) && parentState.IsKeyDown(Keys.Down))
            {
                if (mainMenu.SelectedIndex < mainMenu.Items.Count - 1)
                    mainMenu.SelectedIndex++;
            }
            else if (state.IsKeyUp(Keys.Up) && parentState.IsKeyDown(Keys.Up))
            {
                if (mainMenu.SelectedIndex > 0)
                    mainMenu.SelectedIndex--;
            }
            else if (state.IsKeyUp(Keys.Left) && parentState.IsKeyDown(Keys.Left))
            {
                if (superiorMenu.Tab > 0)
                    superiorMenu.Tab--;
            }
            else if (state.IsKeyUp(Keys.Right) && parentState.IsKeyDown(Keys.Right))
            {
                if (superiorMenu.Tab < 5)
                    superiorMenu.Tab++;
            }

            parentState = state;

        }
    }
}
