using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace RPG_test_game.Game
{
    class Fireball
    {

        private float velocity = 200f;

        public Vector2 Position
        {
            get { return position; }
        }
        private Vector2 position;

        public Vector2 Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        private Vector2 destination;

        public Fireball(Vector2 position, Vector2 destination)
        {
            this.position = position;
            this.destination = destination;
        }

        public void Update(GameTime gameTime)
        {
            if (Helper.Distance(destination, position) > 2)
            {
                var angle = Math.Atan2(position.Y - destination.Y, position.X - destination.X);
                position.X -= (float)Math.Cos(angle) * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position.Y -= (float)Math.Sin(angle) * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                position = destination;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0.0f, new Vector2(texture.Width/2,texture.Height/2), 0.75f, SpriteEffects.None, 0.0f);
        }
    }
}
