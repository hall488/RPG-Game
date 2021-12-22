using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Linq;

namespace RPG_test_game.Game
{
    class Entity
    {

        public Scenario Scenario
        {
            get { return scenario; }
        }
        protected Scenario scenario;

        public Vector2 Position
        {
            get { return position; }
        }
        protected Vector2 position;

        public Vector2 Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        protected Vector2 destination;

        public List<Vector2> MoveQueue
        {
            get { return moveQueue; }
            set { moveQueue = value; destination = scenario.Points[scenario.Coords.IndexOf(moveQueue[0])]; }
        }
        protected List<Vector2> moveQueue;


        public Entity(Vector2 position, Scenario scenario)
        {
            this.position = position;
            this.destination = position;
            this.scenario = scenario;
            moveQueue = new List<Vector2>();
        }

        public List<Vector2> BranchingPath(Vector2 finish, Tile[,] tiles)
        {
            Vector2 move;
            var paths = new List<List<Vector2>>();
            var nodes = new List<Vector2>();
            var start = scenario.Coords[scenario.Points.IndexOf(position)];

            var startPath = new List<Vector2>();
            startPath.Add(start);

            paths.Add(startPath);

            bool target = true;

            while (target)
            {

                for (int i = 0; i < paths.Count; i++)
                {
                    var e = paths[i];

                    for (int j = 0; j < 6; j++)
                    {


                        if (e[e.Count - 1].Y % 2 != 1)
                            switch (j)
                            {
                                case 0:
                                    move = new Vector2((float)0, (float)-1);
                                    break;
                                case 1:
                                    move = new Vector2((float)1, (float)-1);
                                    break;
                                case 2:
                                    move = new Vector2((float)1, (float)0);
                                    break;
                                case 3:
                                    move = new Vector2((float)1, (float)1);
                                    break;
                                case 4:
                                    move = new Vector2((float)0, (float)1);
                                    break;
                                case 5:
                                    move = new Vector2((float)-1, (float)0);
                                    break;
                                default:
                                    move = Vector2.Zero;
                                    break;

                            }

                        else
                        {
                            switch (j)
                            {
                                case 0:
                                    move = new Vector2((float)-1, (float)-1);
                                    break;
                                case 1:
                                    move = new Vector2((float)0, (float)-1);
                                    break;
                                case 2:
                                    move = new Vector2((float)1, (float)0);
                                    break;
                                case 3:
                                    move = new Vector2((float)0, (float)1);
                                    break;
                                case 4:
                                    move = new Vector2((float)-1, (float)1);
                                    break;
                                case 5:
                                    move = new Vector2((float)-1, (float)0);
                                    break;
                                default:
                                    move = Vector2.Zero;
                                    break;

                            }

                        }

                        if (move == new Vector2(1, 0) && tiles[(int)e[e.Count - 1].X, (int)e[e.Count - 1].Y].Collision == TileCollision.NearDoor)
                        {
                            continue;
                        }


                        Vector2 newPos = e[e.Count - 1] + move;

                        if (newPos.X > scenario.Width - 1 || newPos.X < 0 || newPos.Y > scenario.Height - 1 || newPos.Y < 0)
                        {
                            nodes.Add(newPos);
                            continue;
                        }

                        if (move == new Vector2(-1, 0) && tiles[(int)newPos.X, (int)newPos.Y].Collision == TileCollision.NearDoor)
                        {
                            continue;
                        }

                        if (tiles[(int)newPos.X, (int)newPos.Y].Collision != TileCollision.Impassable && !nodes.Contains(newPos))
                        {
                            var f = new List<Vector2>(e);
                            f.Add(newPos);
                            paths.Add(f);

                            if (newPos == finish)
                            {
                                target = false;
                                return f;
                            }
                        }

                        if (j == 5)
                        {
                            nodes.Add(e[e.Count - 1]);
                        }


                    }
                }
            }

            throw new NotSupportedException("Path couldnt be finished");

        }

    }
}
