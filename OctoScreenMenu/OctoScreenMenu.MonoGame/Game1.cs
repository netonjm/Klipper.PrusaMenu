﻿using System;
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

        KMenuModel menuModel;

        List<BaseScreen> screens = new List<BaseScreen>();

        BaseScreen currentScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            var os = PlatformID.MacOSX;

            string homePath = os == PlatformID.Unix ? "/home/pi" :
                       os == PlatformID.MacOSX
        ? Environment.GetEnvironmentVariable("HOME")
        : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            //homePath
            string filePath = $"{homePath}/printer.cfg";

            menuModel = new KMenuModel (filePath);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
          
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            GameContext.Initialize(_graphics, menuModel);

            var loadingScreen = new LoadingScreen();
            loadingScreen.Finished += (s, e) => Show<MainScreen>();
            currentScreen = loadingScreen;

            screens.Add(currentScreen);
            screens.Add(new MainScreen());

            Show<MainScreen>();

            foreach (var screen in screens)
                screen.Initialize ();
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
            currentScreen = new MainScreen();
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