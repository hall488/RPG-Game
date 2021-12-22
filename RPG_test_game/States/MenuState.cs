using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG_test_game.Controls;

namespace RPG_test_game.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        private Texture2D titleTag;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager content)
      : base(game, graphicsDevice, graphics, content)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            var buttonFont = content.Load<SpriteFont>("Fonts/Font");
            titleTag = content.Load<Texture2D>("Menus/Title Tag");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var loadGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Load Game",
            };

            loadGameButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
             {
                newGameButton,
                loadGameButton,
                quitGameButton,
             };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(new Color(30, 30, 30, 255));

            spriteBatch.Begin();

            spriteBatch.Draw(titleTag, new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 4), null, Color.White, 0.0f, new Vector2(titleTag.Width/2, titleTag.Height/2), 1.0f, SpriteEffects.None,0.0f);
            

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Game");
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, graphicsDevice, graphics, content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
}
