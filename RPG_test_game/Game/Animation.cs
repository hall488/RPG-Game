using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG_test_game.Game
{
    class Animation
    {
        public Texture2D Texture //spritesheet for an animation
        {
            get { return texture; }
        }
        private Texture2D texture;

        public float FrameTime //time per sprite on spritesheet
        {
            get { return frameTime; }
        }
        private float frameTime;

        public bool IsLooping //is this a looping animation or a single play
        {
            get { return isLooping; }
        }
        private bool isLooping;

        public int FrameCount //number of frames in spritesheet
        {
            get { return Texture.Width / FrameWidth; }
        }
        private int frameCount;

        public int FrameWidth //width of a single frame in spritesheet
        {
            get { return frameWidth; }
        }
        private int frameWidth;

        public int FrameHeight //height of a single frame in spritesheet
        {
            get { return Texture.Height; }
        }
        private int frameHeight;

        public Animation(Texture2D texture, float frameTime, int frameWidth, bool isLooping) //constructor
        {
            this.texture = texture;
            this.frameTime = frameTime;
            this.frameWidth = frameWidth;
            this.isLooping = isLooping;
        }
    }

    
    
}
