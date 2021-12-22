using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Linq;
using RPG_test_game.Serialization;

namespace RPG_test_game.Game
{
    class Player : Entity
    {

        private Animation idleBlinkingAnimation;
        private Animation walkingAnimation;
        private Animation castingAnimation;
        private AnimationPlayer sprite;
        private Texture2D fireball_texture;
        private List<Texture2D> cardTextures;

        private SpriteEffects flip = SpriteEffects.None;
        private float velocity = 150f;

        public List<Fireball> Fireballs
        {
            get { return fireballs; }
        }
        private List<Fireball> fireballs;

        public Hand Hand
        {
            get { return hand; }
        }
        private Hand hand;

        public Player(Vector2 position, Scenario scenario) : base(position, scenario)
        {
            fireballs = new List<Fireball>();
            hand = new Hand(new List<Card>(), scenario);
            cardTextures = new List<Texture2D>();

            List<Card> cards = JsonSerialization.ReadObjectFromJsonFile<List<Card>>("Content/Cards/Cards.json", "Wizard Cards");
            foreach (Card e in cards)
                hand.AddCard(e);

            LoadContent();
        }

       
        public void LoadContent()
        {
            idleBlinkingAnimation = new Animation(scenario.Content.Load<Texture2D>("Sprites/Wizard - 02/PNG Sequences/Idle Blinking/Idle Blinking_SS"), 0.1f, 400, true);
            walkingAnimation = new Animation(scenario.Content.Load<Texture2D>("Sprites/Wizard - 02/PNG Sequences/Walking/Walking_SS"), 0.1f, 400, true);
            castingAnimation = new Animation(scenario.Content.Load<Texture2D>("Sprites/Wizard - 02/PNG Sequences/Casting Spells/Casting Spells_SS"), 0.08f, 400, true);
            fireball_texture = scenario.Content.Load<Texture2D>("Sprites/Wizard - 02/Game Objects/Fireball Effect");
            
            
        }

        public void Update(KeyboardState kstate, MouseState mstate, GameTime gameTime)
        {

            //casting code
            if(mstate.RightButton == ButtonState.Pressed)
            {
                sprite.PlayAnimation(castingAnimation);
            }
            else if(Helper.Distance(destination,position) > 1.5)
            {
                var angle = Math.Atan2(position.Y - destination.Y, position.X - destination.X);
                if (Math.Abs(angle) >= Math.PI / 2)
                    flip = SpriteEffects.None;
                else
                    flip = SpriteEffects.FlipHorizontally;
                position.X -= (float)Math.Cos(angle) * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position.Y -= (float)Math.Sin(angle) * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                sprite.PlayAnimation(walkingAnimation);
            } 
            else if(position != destination && moveQueue.Count != 0)
            {
                moveQueue.ForEach(i => Debug.WriteLine(i));
                position = destination;
                moveQueue.Remove(destination);
            }
            else if(position == destination && moveQueue.Count != 0)
            {
                moveQueue.ForEach(i => Debug.WriteLine(i));
                destination = scenario.Points[scenario.Coords.IndexOf(moveQueue[0])];
                moveQueue.Remove(moveQueue[0]);
            }
            else
            {
                position = destination;
                sprite.PlayAnimation(idleBlinkingAnimation);
            }
                


            //fireball code
            for (int i = fireballs.Count - 1; i >= 0; i--)
            {
                fireballs[i].Update(gameTime);
                if (fireballs[i].Position == fireballs[i].Destination)
                {
                    for (int j = scenario.Enemies.Count - 1; j >= 0; j--)
                    {
                        if (scenario.Enemies[j].Position == fireballs[i].Position)
                            scenario.RemoveEnemy(scenario.Enemies[j]);
                    }
                    DestroyFireball(fireballs[i]);                    
                }

            }


        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            sprite.Draw(gameTime, _spriteBatch, position, flip);
            foreach (Fireball element in fireballs)
            {
                element.Draw(gameTime, _spriteBatch, fireball_texture);
            }
            
        }

        public void CreateFireball(Vector2 destination)
        {
            Fireball fireball = new Fireball(position, destination);
            fireballs.Add(fireball);
        }

        public void DestroyFireball(Fireball e)
        {
            fireballs.Remove(e);
        }

       
    }
}
