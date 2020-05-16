using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public class MonoGameView : IMonoGameObject
    {
        public IReadOnlyList<MonoGameView> Children => children.AsReadOnly();
        readonly List<MonoGameView> children = new List<MonoGameView>();

        public void AddChildren (MonoGameView view)
        {
            if (view.parent != null)
            {
                throw new Exception("already a parent");
            }

            if (children.Contains(view))
            {
                throw new Exception("already in the collection");
            }

            view.parent = this;
            children.Add(view);
        }

        public void RemoveChildren(MonoGameView view)
        {
           if (children.Contains (view)) {
                view.parent = null;
                children.Remove(view);
           }
        }

        MonoGameView parent;
        public MonoGameView Parent
        {
            get => parent;
            set
            {
                parent = value;
                OnNeedsRedraw();
            }
        }

        Rectangle allocation = Rectangle.Empty;
        public Rectangle Allocation
        {
            get => allocation;
            set
            {
                allocation = value;
                OnNeedsRedraw();
            }
        }

        public Rectangle AbsoluteBounds {
            get
            {
                if (Parent == null)
                    return allocation;

                return new Rectangle(
                    Parent.allocation.X + Allocation.X,
                     Parent.allocation.Y + Allocation.Y,
                      Allocation.Width,
                        Allocation.Height
                    );
            }
        }

        internal void Initialize()
        {
           
        }

        public Point Position
        {
            get => allocation.Location;
            set
            {
                allocation.Location = value;
                OnNeedsRedraw();
            }
        }

        public Point Size
        {
            get => allocation.Size;
            set
            {
                allocation.Size = value;
                OnNeedsRedraw();
            }
        }

        public int Width
        {
            get => allocation.Width;
            set
            {
                allocation.Width = value;
                OnNeedsRedraw();
            }
        }

        public int Height
        {
            get => allocation.Height;
            set
            {
                allocation.Height = value;
                OnNeedsRedraw();
            }
        }

        protected virtual void OnNeedsRedraw()
        {

        }

        public virtual void LoadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
