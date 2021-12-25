using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GYMARB_PacMan
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Texture2D pmTexture;
        Vector2 pmPosition = new Vector2(100, 100);
        Rectangle pmBox;
        float pmSpeed = 3f;
        Vector2 pmVelocity;

        Texture2D coinTexture;
        Vector2 coinPosition = new Vector2(200, 20);

        Texture2D testTexture;
        Rectangle wall;
        SpriteFont font;
        int points = 0;

        private List<Rectangle> coins;
        private List<Rectangle> walls;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            coins = new List<Rectangle>();
            walls = new List<Rectangle>();



            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pmTexture = Content.Load<Texture2D>("Pacman");
            coinTexture = Content.Load<Texture2D>("Coin");
            testTexture = Content.Load<Texture2D>("Test");
            font = Content.Load<SpriteFont>("font");



            for (int i = 0; i < 20; i++)
            {
                coins.Add(new Rectangle((int)coinPosition.X, (int)coinPosition.Y, 5, 5));
                coinPosition.X += 20;
            }



            wall = new Rectangle(300, 300, testTexture.Width, testTexture.Height);

        }
        
        bool TouchingLeft(Rectangle touch)
        {
            return pmBox.Right + pmVelocity.X > touch.Left &&
                pmBox.Left < touch.Left &&
                pmBox.Bottom > touch.Top &&
                pmBox.Top < touch.Bottom;
        }
        bool TouchingRight(Rectangle touch)
        {
            return pmBox.Left + pmVelocity.X < touch.Right &&
                pmBox.Right > touch.Right &&
                pmBox.Bottom > touch.Top &&
                pmBox.Top < touch.Bottom;
        }
        bool TouchingTop(Rectangle touch)
        {
            return pmBox.Bottom + pmVelocity.Y > touch.Top &&
                pmBox.Top < touch.Top &&
                pmBox.Right > touch.Left &&
                pmBox.Left < touch.Right;
        }
        bool TouchingBottom(Rectangle touch)
        {
            return pmBox.Top + pmVelocity.Y < touch.Bottom &&
                pmBox.Bottom > touch.Bottom &&
                pmBox.Right > touch.Left &&
                pmBox.Left < touch.Right;
        }
        
        public static Rectangle Intersection(Rectangle r1, Rectangle r2)
        {
            int x1 = Math.Max(r1.Left, r2.Left);
            int y1 = Math.Max(r1.Top, r2.Top);
            int x2 = Math.Min(r1.Right, r2.Right);
            int y2 = Math.Min(r1.Bottom, r2.Bottom);

            if ((x2 >= x1) && (y2 >= y1))
        {
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }
            return Rectangle.Empty;
        }

        protected override void Update(GameTime gameTime)
        {
            pmBox = new Rectangle((int)pmPosition.X, (int)pmPosition.Y, 15, 15);

            

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                pmVelocity.X = -pmSpeed;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                pmVelocity.X = pmSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                pmVelocity.Y = -pmSpeed;
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                pmVelocity.Y = pmSpeed;

            if (pmVelocity.X > 0 && TouchingLeft(wall) || pmVelocity.X < 0 && TouchingRight(wall))
                pmVelocity.X = 0;
            if (pmVelocity.Y > 0 && TouchingTop(wall) || pmVelocity.Y < 0 && TouchingBottom(wall))
                pmVelocity.Y = 0;

            pmPosition += pmVelocity;
            pmVelocity = Vector2.Zero;



            int i = 0;
                foreach (var coin in coins)
                {
                    if (pmBox.Intersects(coin))
                    {
                        points++;
                        coins.RemoveAt(i);
                        break;
                    }
                i++;
                }
            i = 0;





            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin();

            spriteBatch.Draw(pmTexture, pmPosition, Color.White);

            spriteBatch.Draw(testTexture, new Vector2(300, 300), Color.Red);

            foreach (var coin in coins)
            {
                spriteBatch.Draw(coinTexture, coin, Color.White);
            }

            spriteBatch.DrawString(font, points.ToString(), new Vector2(10, 20), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
