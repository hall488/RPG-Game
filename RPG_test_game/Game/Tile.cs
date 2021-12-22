using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace RPG_test_game.Game
{
    enum TileCollision
    {
        Impassable = 0,
        Passable = 1, 
        NearDoor = 2,
    }

    struct Tile
    {

        public TileCollision Collision;

        public const int Width = 97;
        public const int Height = 115;

        public static readonly Vector2 Size = new Vector2(97, 115);

        public Tile(TileCollision collision)
        {
            Collision = collision;
        }

    }
}
