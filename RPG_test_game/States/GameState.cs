using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using RPG_test_game.Game;

namespace RPG_test_game.States
{
    public class GameState : State
    {
        private SpriteBatch _spriteBatch;

        private Scenario scenario;
        private Camera camera;
        private Texture2D rect;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager content) : base(game, graphicsDevice, graphics, content)
        {
            //_graphics = new GraphicsDeviceManager(game);
            //Content.RootDirectory = "Content";
            //IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = graphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = graphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            camera = new Camera(graphicsDevice.Viewport);

            var scenarioName = "Black Barrow";
            string scenarioPath = string.Format("Content/Scenarios/{0}.txt", scenarioName);
            using (Stream fileStream = TitleContainer.OpenStream(scenarioPath))
                scenario = new Scenario(game.Services, camera, fileStream, scenarioName, graphicsDevice.Viewport.Bounds);

            LoadContent();
        }

        public void Initialize()
        {

            
            //TODO: Add your initialization logic here






        }

        public void LoadContent()
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);            

            scenario.LoadContent(graphicsDevice, content);
            Debug.WriteLine(graphicsDevice.Viewport.Bounds.X);

            rect = new Texture2D(graphicsDevice, graphics.PreferredBackBufferWidth, scenario.Player.Hand.CardHeight + 50);
            Color[] data = new Color[(scenario.Player.Hand.CardHeight + 50) * graphics.PreferredBackBufferWidth];
            for (int i = 0; i < data.Length; ++i) data[i] = new Color(10, 10, 10, 255);
            rect.SetData(data);
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();

            // TODO: Add your update logic here
            camera.UpdateCamera(graphicsDevice.Viewport);

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();
            
            scenario.Update(kstate, mstate, gameTime, graphicsDevice);
            

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //darkish gray
            graphicsDevice.Clear(new Color(30, 30, 30, 255));

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            scenario.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
            //spriteBatch.Draw(rect, new Vector2(0, graphics.PreferredBackBufferHeight - scenario.Player.Hand.CardHeight - 50), Color.White); 
            //scenario.Player.Hand.Draw(gameTime, spriteBatch, graphicsDevice);
            spriteBatch.End();

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
    }
}
