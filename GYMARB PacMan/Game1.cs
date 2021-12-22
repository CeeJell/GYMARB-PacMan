using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GYMARB_Test
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
        Rectangle coinBox;

        Texture2D testTexture;
        Rectangle testBox1;
        Rectangle testBox2;

        SpriteFont font;
        int points = 0;

        private List<Vector2> coins;
        private List<Rectangle> walls;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            coins = new List<Vector2>();
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



            coins.Add(new Vector2(coinPosition.X, coinPosition.Y));
            coinBox = new Rectangle((int)coinPosition.X, (int)coinPosition.Y, 5, 5);

            testBox1 = new Rectangle(300, 300, 250, 250);

        }

        bool TouchingLeft()
        {
            return pmBox.Right + pmVelocity.X > testBox1.Left &&
                pmBox.Left < testBox1.Left &&
                pmBox.Bottom > testBox1.Top &&
                pmBox.Top < testBox1.Bottom;
        }
        bool TouchingRight()
        {
            return pmBox.Left + pmVelocity.X < testBox1.Right &&
                pmBox.Right > testBox1.Right &&
                pmBox.Bottom > testBox1.Top &&
                pmBox.Top < testBox1.Bottom;
        }
        bool TouchingTop()
        {
            return pmBox.Bottom + pmVelocity.Y > testBox1.Top &&
                pmBox.Top < testBox1.Top &&
                pmBox.Right > testBox1.Left &&
                pmBox.Left < testBox1.Right;
        }
        bool TouchingBottom()
        {
            return pmBox.Top + pmVelocity.Y < testBox1.Bottom &&
                pmBox.Bottom > testBox1.Bottom &&
                pmBox.Right > testBox1.Left &&
                pmBox.Left < testBox1.Right;
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

            if (pmVelocity.X > 0 && TouchingLeft() || pmVelocity.X < 0 && TouchingRight())
                pmVelocity.X = 0;
            if (pmVelocity.Y > 0 && TouchingTop() || pmVelocity.Y < 0 && TouchingBottom())
                pmVelocity.Y = 0;

            pmPosition += pmVelocity;
            pmVelocity = Vector2.Zero;


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
