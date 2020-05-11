using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TrueTypeSharp;

namespace TestApplication
{

    public class Game1 : Game
    {
        SpriteBatch spriteBatch;
        VerticalMenu mainMenu;
        Texture2D background;
        GraphicsDeviceManager _graphics;

        SuperiorMenu superiorMenu;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainMenu = new VerticalMenu(_graphics);
            mainMenu.Position = new Point(100, 200);
            mainMenu.Add("hola");
            var menu = mainMenu.Add("menu");
            mainMenu.Add("mi casa");
            mainMenu.Add("test");
            mainMenu.Add("lol");

            superiorMenu = new SuperiorMenu(_graphics);
            superiorMenu.Position = new Point(0, 20);

            background = _graphics.CreateTexture2D ("background.png");
        }

        KeyboardState parentState;

        protected override void Update(GameTime gameTime)
        {
            // Poll for current keyboard state
            KeyboardState state = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || state.IsKeyDown(Keys.Escape))
                Exit();

            if (state.IsKeyUp (Keys.Down) && parentState.IsKeyDown (Keys.Down)) {
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

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            superiorMenu.Draw(spriteBatch);
            mainMenu.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
