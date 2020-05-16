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
            superiorMenu.OffsetFirstItem = 40;
            superiorMenu.ItemWidth = 90;
            superiorMenu.ItemHeight = 26;
            superiorMenu.Position = new Point(0, 23);

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
                menuListView.Add("Parent".ToUpper(), menuModel.Parent, refresh: false);

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

            base.Draw(gameTime);
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
            else if (state.IsKeyUp(Keys.Back) && parentState.IsKeyDown(Keys.Back))
            {
                BackFile();
            }

            parentState = state;
        }

        private void BackFile()
        {
            if (!menuModel.IsMain)
                menuModel.Actual = menuModel.Parent;
            RefreshItems();
        }

        private void SelectCurrentFile()
        {
            if (menuListView.SelectedIndex == -1)
                return;
            if (menuListView.SelectedItem is OctoScreenMenu.MenuSectionFile file)
                menuModel.Actual = file;
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
            if (superiorMenu.SelectedIndex < 5)
                superiorMenu.SelectedIndex++;
           
        }

        void SelectPreviousTab()
        {
            if (superiorMenu.SelectedIndex > 0)
                superiorMenu.SelectedIndex--;
        }
    }
}
