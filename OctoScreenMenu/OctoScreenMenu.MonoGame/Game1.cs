using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TrueTypeSharp;
using System.Linq;

namespace TestApplication
{
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager _graphics;

        List<BaseScreen> screens = new List<BaseScreen>();

        BaseScreen currentScreen;

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

            var loadingScreen = new LoadingScreen(_graphics);
            loadingScreen.Finished += (s,e) => Show<MainScreen>();
            currentScreen = loadingScreen;

            screens.Add(currentScreen);
            screens.Add(new MainScreen(_graphics));

            Show<LoadingScreen>();
        }

        protected override void LoadContent()
        {
            foreach (var screen in screens)
                screen.LoadContent();
        }

        void Show <T> () where T : BaseScreen
        {
            currentScreen = screens.OfType<T>().FirstOrDefault();
        }

        private void LoadingScreen_Finished(object sender, EventArgs e)
        {
            currentScreen = new MainScreen(_graphics);
            currentScreen.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            // Poll for current keyboard state
            KeyboardState state = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || state.IsKeyDown(Keys.Escape))
                Exit ();

            currentScreen?.Update(gameTime);

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            currentScreen?.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
