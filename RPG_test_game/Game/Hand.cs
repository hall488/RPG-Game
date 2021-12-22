using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace RPG_test_game.Game
{
    class Hand
    {
        public List<Texture2D> CardTextures
        {
            get { return cardTextures; }
        }
        private List<Texture2D> cardTextures;

        public int CardWidth
        {
            get { return cardWidth;  }
        }
        private int cardWidth = 270;

        public int CardHeight
        {
            get { return cardHeight; }
        }
        private int cardHeight= 370;

        public List<Vector2> CardPositions
        {
            get { return cardPositions; }
        }
        private List<Vector2> cardPositions;

        public List<Card> Cards
        {
            get { return cards; }
        }
        private List<Card> cards;

        private ContentManager content;
        private Scenario scenario;

        public Hand(List<Card> cards, Scenario scenario)
        {
            this.content = scenario.Content;
            this.cards = cards;
            this.scenario = scenario;
            cardTextures = new List<Texture2D>();
            cardPositions = new List<Vector2>();

            //foreach (Card e in cards)
            //{
            //    cardTextures.Add(content.Load<Texture2D>("Cards/" + e.name));
            //}

            for (int i = 0; i < cards.Count; i++)
            {
                cardTextures.Add(content.Load<Texture2D>("Cards/" + cards[i].name));
                cardPositions.Add(new Vector2((cardWidth + 5) * i + cardWidth, scenario.Bounds.Height - cardHeight - 25));
            }
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
            cardTextures.Add(content.Load<Texture2D>("Cards/" + card.name));
            cardPositions.Add(new Vector2((cardWidth + 5) * cards.Count + cardWidth, scenario.Bounds.Height - cardHeight - 25));
        }

        public void Update(MouseState mState)
        {
            //foreach (Card e in hand)
            //{
            //    Helper.Distance(tilePos, mPos) > Helper.Distance(element, mPos) && mstate.Position.Y < graphics.Viewport.Bounds.Height - player.Hand.CardHeight - 25
            //}

            for (int i = 0; i < cardPositions.Count; i++)
            {
                
                var e = cardPositions[i];
                if ( mState.Position.X >= e.X && mState.Position.X <= e.X + cardWidth)
                {

                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, GraphicsDevice _graphics)
        {
            Debug.WriteLine(cards);
            Debug.WriteLine(cardTextures);
            if(cardTextures.Count == cards.Count)
            {
                for (int i = 0; i < Cards.Count; i++)
                {
                    _spriteBatch.Draw(cardTextures[i], new Vector2((cardWidth + 5) * i + cardWidth, _graphics.Viewport.Bounds.Height - cardHeight -25), null, Color.White, 0.0f, new Vector2(cardWidth / 2, 0), 0.5f, SpriteEffects.None, 0.0f);
                }
            }
            
        }

        

    }
}
