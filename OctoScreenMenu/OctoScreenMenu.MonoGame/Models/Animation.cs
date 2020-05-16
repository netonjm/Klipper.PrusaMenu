using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OctoScreenMenu.MonoGame.Models
{
    public class AnimationFrame
    {
        public int CurrentFrame { get; set; }

        public int FrameCount { get; private set; }

        public int FrameHeight { get { return Texture.Height; } }

        public float FrameSpeed { get; set; }

        public int FrameWidth { get { return Texture.Width / FrameCount; } }

        public bool IsLooping { get; set; }

        public Texture2D Texture { get; private set; }

        public AnimationFrame(Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            IsLooping = true;

            FrameSpeed = 0.2f;
        }
    }

    public class Animation
    {
        private float _timer;

        public List<AnimationFrame> Frames = new List<AnimationFrame>();
        public string Name { get; set; }

        public Animation(string name)
        {
            Name = name;
        }

        public int FramesPerSecond = 3;

        public AnimationFrame CurrentFrame;

        int currentIndex;
        public int CurrentFrameIndex
        {
            get => currentIndex;
            set
            {
                currentIndex =value;
            }
        }

        public void Play ()
        {
           // _animation.CurrentFrame = 0;

            _timer = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > FramesPerSecond)
            {
                _timer = 0f;

                //if ()

                //_animation.CurrentFrame++;

                //if (_animation.CurrentFrame >= _animation.FrameCount)
                //    _animation.CurrentFrame = 0;
            }
        }

        public void Stop()
        {
            _timer = 0f;

            //_animation.CurrentFrame = 0;
        }

    }

}
