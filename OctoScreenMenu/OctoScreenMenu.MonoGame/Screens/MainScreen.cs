using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestApplication
{
    public class MainScreen : BaseScreen
    {
        SpriteBatch spriteBatch;
        MenuListView menuListView;
        Texture2D background;
        Texture2D over;
        SuperiorMenuView superiorMenu;

        KeyboardState parentState;

        KMenuModel menuModel;

        public MainScreen() 
        {
          
        }

        public override void Initialize()
        {
            menuModel = GameContext.Menu;

            spriteBatch = new SpriteBatch(GameContext.GraphicsDevice);
            menuListView = new MenuListView();
            menuListView.Position = new Point(30, 40);
            menuListView.Separation = 0;

            superiorMenu = new SuperiorMenuView();
            superiorMenu.Position = new Point(0, 0);

            background = GameContext.CreateTexture2D("background.png");

            over = GameContext.CreateTexture2D("over.png");

            RefreshItems();

            base.Initialize();
        }

        void RefreshItems()
        {
            menuListView.Clear(false);
            if (menuModel.IsMain)
                menuListView.Add("Main".ToUpper(), null, refresh: false);
            else
                menuListView.Add("Parent...".ToUpper(), menuModel.Parent, refresh: false);

            foreach (var item in menuModel.Children)
            {
                if (string.IsNullOrEmpty(item.RespondAction))
                    menuListView.Add(item.Title.ToUpper(), item, refresh: false);
            }
            menuListView.Refresh();
            if (menuListView.Items.Count > 0)
                menuListView.SelectedIndex = 0;
            else
                menuListView.SelectedIndex = -1;
        }

        public override void Draw(GameTime gameTime)
        {
            GameContext.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, GameContext.Width, GameContext.Height), Color.White);
            superiorMenu.Draw(gameTime, spriteBatch);
            menuListView.Draw(gameTime, spriteBatch);

            //spriteBatch.Draw(over, new Rectangle(0, 0, GameContext.Width, GameContext.Height), Color.White);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            // Poll for current keyboard state
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyUp(Keys.Down) && parentState.IsKeyDown(Keys.Down))
            {
                SelectNextFile();
            }
            else if (state.IsKeyUp(Keys.Up) && parentState.IsKeyDown(Keys.Up))
            {
                SelectPreviousFile();
            }
            else if (state.IsKeyUp(Keys.Left) && parentState.IsKeyDown(Keys.Left))
            {
                SelectPreviousTab();
            }
            else if (state.IsKeyUp(Keys.Right) && parentState.IsKeyDown(Keys.Right))
            {
                SelectNextTab();
            }
            else if (state.IsKeyUp(Keys.Enter) && parentState.IsKeyDown(Keys.Enter))
            {
                SelectCurrentFile ();
            }

            parentState = state;
        }

        private void SelectCurrentFile()
        {
            if (menuListView.SelectedIndex == -1)
                return;
            var item = menuListView.SelectedItem as OctoScreenMenu.MenuSectionFile;
            if (item != null)
            menuModel.Actual = item;

            RefreshItems();
        }

        void SelectNextFile()
        {
            if (menuListView.SelectedIndex < menuListView.Items.Count - 1)
                menuListView.SelectedIndex++;
        }

        void SelectPreviousFile ()
        {
            if (menuListView.SelectedIndex > 0)
            {
                menuListView.SelectedIndex--;
            }
        }

        void SelectNextTab ()
        {
            if (superiorMenu.Tab > 0)
                superiorMenu.Tab--;
        }

        void SelectPreviousTab()
        {
            if (superiorMenu.Tab < 5)
                superiorMenu.Tab++;
        }

    }
}
