using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG_test_game.Game
{
    class Enemy : Entity
    {

        private Animation idleBlinkingAnimation;
        private Animation walkingAnimation;
        private AnimationPlayer sprite;

        private SpriteEffects flip = SpriteEffects.None;
        private float velocity = 100f;

        public Enemy(Vector2 position, Scenario scenario) : base(position, scenario)
        {

            LoadContent();
        }


        public void LoadContent()
        {
            idleBlinkingAnimation = new Animation(scenario.Content.Load<Texture2D>("Sprites/Regular_Skeleton_01/PNG Sequences/Idle Blink/Idle Blinking_SS"), 0.1f, 520, true);
            walkingAnimation = new Animation(scenario.Content.Load<Texture2D>("Sprites/Regular_Skeleton_01/PNG Sequences/Walking/Walking_SS"), 0.1f, 520, true);            
        }

        public void Update(GameTime gameTime)
        {
            sprite.PlayAnimation(idleBlinkingAnimation);

            
            //if (position.Y > _graphics.PreferredBackBufferHeight - sprite.Animation.FrameHeight / 2)
            //    position.Y = _graphics.PreferredBackBufferHeight - sprite.Animation.FrameHeight / 2;
            //else if (position.Y < sprite.Animation.FrameHeight / 2)
            //    position.Y = sprite.Animation.FrameHeight / 2;

            //if (position.X > _graphics.PreferredBackBufferWidth - sprite.Animation.FrameWidth / 2)
            //    position.X = _graphics.PreferredBackBufferWidth - sprite.Animation.FrameWidth / 2;
            //else if (position.X < sprite.Animation.FrameWidth / 2)

            //    position.X = sprite.Animation.FrameWidth / 2;
            


        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            sprite.Draw(gameTime, _spriteBatch, position, flip);
        }
    }
}
