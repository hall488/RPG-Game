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
    class Scenario
    {

        public Camera Camera
        {
            get { return camera; }
        }
        private Camera camera;

        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Texture2D Texture
        {
            get { return texture; }
        }
        private Texture2D texture;

        public Vector2 Origin
        {
            get { return new Vector2(texture.Width / 2.0f, texture.Height / 2.0f); }
        }

        public Vector2 Start
        {
            get { return start; }
        }
        private Vector2 start;

        public List<Vector2> Points
        {
            get { return points; }
        }
        private List<Vector2> points;

        public List<Vector2> Coords
        {
            get { return coords; }
        }
        private List<Vector2> coords;

        public List<Enemy> Enemies
        {
            get { return enemies; }
        }
        private List<Enemy> enemies;


        public Tile[,] Tiles
        {
            get { return tiles; }
        }
        private Tile[,] tiles;
        public int Width
        {
            get { return tiles.GetLength(0); }
        }
        public int Height
        {
            get { return tiles.GetLength(1); }
        }

        public Player Player
        {
            get { return player; }
        }
        private Player player;

        public Rectangle Bounds
        {
            get { return bounds; }
        }
        private Rectangle bounds;


        private Vector2 center;
        private Rectangle rect;
        private Vector2 hex = new Vector2(97, 115);
        private int newHexLine = 85;
        private Texture2D tile;
        private Vector2 tilePos;
        private float lastTimeCheck = 0;

        public Scenario(IServiceProvider serviceProvider, Camera camera, Stream fileStream, String scenarioName, Rectangle bounds)
        {
            this.bounds = bounds;

            content = new ContentManager(serviceProvider, "Content");
            this.camera = camera;

            this.center = new Vector2(camera.Bounds.Width / 2, camera.Bounds.Height / 2);

            LoadScenario(fileStream);

        }

        private void LoadScenario(Stream fileStream)
        {
            int width;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                Debug.WriteLine(line);
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
                    line = reader.ReadLine();
                }
            }

            // Allocate the tile grid.
            tiles = new Tile[width, lines.Count];
            points = new List<Vector2>();
            coords = new List<Vector2>();
            enemies = new List<Enemy>();

            texture = content.Load<Texture2D>("Sprites/Gloomhaven Tiles/Black Barrow");
            tile = content.Load<Texture2D>("Sprites/Gloomhaven Tiles/Tile_Highlight");
            rect = new Rectangle(0, 0, texture.Width, texture.Height);

            start = new Vector2(center.X - texture.Width / 4 + 124 / 2, center.Y - texture.Height / 4 + 226 / 2);

            // Loop over every tile position,
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // to load each tile.
                    char tileType = lines[y][x];

                    if (tileType != '0')
                    {
                        if (y % 2 != 1)
                        {
                            points.Add(new Vector2(start.X + hex.X * x, start.Y + newHexLine * y));
                            coords.Add(new Vector2(x, y));
                        }
                        else
                        {
                            points.Add(new Vector2(start.X + hex.X * x - hex.X / 2, start.Y + newHexLine * y));
                            coords.Add(new Vector2(x, y));
                        }
                    }

                    tiles[x, y] = LoadTile(tileType, x, y);

                    
                }
            }
        }

        private Tile LoadTile(char tileType, int x, int y)
        {
            switch (tileType)
            {
                case '0':
                    return new Tile(TileCollision.Impassable);
                case '1':
                    return new Tile(TileCollision.Passable);
                case 'D':
                    return new Tile(TileCollision.Passable);
                case 'E':
                    enemies.Add(new Enemy(points[points.Count - 1], this));
                    return new Tile(TileCollision.Passable);
                case 'N':
                    return new Tile(TileCollision.NearDoor);
                case 'P':
                    player = new Player(points[points.Count - 1], this);
                    return new Tile(TileCollision.Passable);
                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y));
            }
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {

            
        }

        public void Update(KeyboardState kstate, MouseState mstate, GameTime gameTime, GraphicsDevice graphics)
        {
            Vector2 mPos = new Vector2(mstate.Position.X, mstate.Position.Y);
            mPos = Vector2.Transform(mPos, Matrix.Invert(camera.Transform));

            foreach (Vector2 element in points)
            {
                if (Helper.Distance(tilePos, mPos) > Helper.Distance(element, mPos) && mstate.Position.Y < graphics.Viewport.Bounds.Height - player.Hand.CardHeight - 25)
                {
                    tilePos = element;
                }
            }

            if(mstate.LeftButton == ButtonState.Pressed && mstate.Position.Y < graphics.Viewport.Bounds.Height - player.Hand.CardHeight - 25)
            {
                //player.Destination = tilePos;
                if (player.MoveQueue.Count == 0 && player.Destination == player.Position)
                {
                    player.MoveQueue = player.BranchingPath(coords[points.IndexOf(tilePos)], tiles);
                    Debug.WriteLine(coords[points.IndexOf(tilePos)]);
                    
                }
            }

            if(mstate.RightButton == ButtonState.Pressed && 1000f < (float)gameTime.TotalGameTime.TotalMilliseconds - lastTimeCheck)
            {
                player.CreateFireball(tilePos);
                lastTimeCheck = (float)gameTime.TotalGameTime.TotalMilliseconds;
                
            }

            foreach (Enemy e in enemies)
            {
                e.Update(gameTime);
            }

            player.Update(kstate, mstate, gameTime);
                
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, center, null, Color.White, 0.0f, Origin, 0.5f, SpriteEffects.None, 0.0f);
            
            spriteBatch.Draw(tile, tilePos, null, Color.White, 0.0f, hex, 0.5f, SpriteEffects.None, 0.0f);

            foreach (Enemy e in enemies)
            {
                e.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);
               
        }

        public void RemoveEnemy(Enemy e)
        {
            enemies.Remove(e);
        }

    }
}
