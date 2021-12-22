using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GYMARB_PacMan.Models.Sprites
{
    public class Wall
    {
        protected Texture2D _texture;

        public Vector2 Position;
        public Vector2 Velocity;
        public Color Colour = Color.White;
        public float Speed;
        public Input Input;
        
        

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public Wall(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gametime, List<Wall> walls)
        { }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, Position, Colour);
        }

        protected bool IsTouchingLeft(Wall wall)
        {
            return this.Rectangle.Right + this.Velocity.X > wall.Rectangle.Left &&
                this.Rectangle.Left < wall.Rectangle.Left &&
                this.Rectangle.Bottom > wall.Rectangle.Top &&
                this.Rectangle.Top < wall.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Wall wall)
        {
            return this.Rectangle.Left + this.Velocity.X < wall.Rectangle.Right &&
                this.Rectangle.Right > wall.Rectangle.Right &&
                this.Rectangle.Bottom > wall.Rectangle.Top &&
                this.Rectangle.Top < wall.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Wall wall)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > wall.Rectangle.Top &&
                this.Rectangle.Top < wall.Rectangle.Top &&
                this.Rectangle.Right > wall.Rectangle.Left &&
                this.Rectangle.Left < wall.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Wall wall)
        {
            return this.Rectangle.Top + this.Velocity.Y < wall.Rectangle.Bottom &&
                this.Rectangle.Bottom > wall.Rectangle.Bottom &&
                this.Rectangle.Right > wall.Rectangle.Left &&
                this.Rectangle.Left < wall.Rectangle.Right;
        }

        protected bool Collect(Wall coinBox)
        {
            return this.Rectangle.Top + this.Velocity.Y < coinBox.Rectangle.Bottom &&
                this.Rectangle.Bottom > coinBox.Rectangle.Bottom &&
                this.Rectangle.Right > coinBox.Rectangle.Left &&
                this.Rectangle.Left < coinBox.Rectangle.Right;
        }
    }
}
