using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG_test_game.States
{
    public abstract class State
    {
        protected ContentManager content;
        protected GraphicsDevice graphicsDevice;
        protected GraphicsDeviceManager graphics;
        protected Game1 game;
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void PostUpdate(GameTime gametime);
        public State(Game1 game, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager content)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
            this.graphics = graphics;
            this.content = content;
        }

        public abstract void Update(GameTime gameTime);

    }
}
