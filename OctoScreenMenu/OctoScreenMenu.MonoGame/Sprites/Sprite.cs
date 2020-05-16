using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OctoScreenMenu.MonoGame.Models;
using TestApplication;

namespace OctoScreenMenu.MonoGame.Sprites
{
    public class Sprite
    {
        #region Fields

        protected AnimationManager _animationManager;

        protected Animation[] _animations;

        protected Vector2 _position;

        protected Texture2D _texture;

        #endregion

        #region Properties

        public Keys Input;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public float Speed = 1f;

        public Vector2 Velocity;

        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("This ain't right..!");
        }

        Animation SelectedAnimation;

        protected virtual void SetAnimations()
        {
            //_animationManager.Play(_animations.First ().Frames.First ());
        }

        public Sprite(Animation[] animations)
        {
            _animations = animations;
            SelectedAnimation = _animations.First();
            //_animationManager = new AnimationManager(SelectedAnimation.Frames.First());
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            SetAnimations();

            _animationManager.Update(gameTime);

            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        #endregion
    }
}
