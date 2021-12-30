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

        Texture2D gRedTexture;
        Vector2 gRedPosition = new Vector2(100, 400);
        Rectangle gRedBox;

        Texture2D coinTexture;
        Vector2 coinPosition = new Vector2(200, 20);

        Texture2D powerTexture;
        Vector2 powerPosition = new Vector2(200, 100);
        double powerTimer = 0;

        Texture2D testTexture;
        SpriteFont font;
        int points = 0;

        //walls
        Texture2D wallTopBot;
        Texture2D wallRightLeft;
        Texture2D wallSidebump;

        private List<Rectangle> coins;
        private List<Rectangle> powers;
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
            powers = new List<Rectangle>();
            walls = new List<Rectangle>();


            // 420w x 405h 

            graphics.PreferredBackBufferWidth = 1420;
            graphics.PreferredBackBufferHeight = 605;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pmTexture = Content.Load<Texture2D>("Pacman");
            gRedTexture = Content.Load<Texture2D>("GhostRed");
            coinTexture = Content.Load<Texture2D>("Coin");
            powerTexture = Content.Load<Texture2D>("PowerUp");
            testTexture = Content.Load<Texture2D>("Test");
            font = Content.Load<SpriteFont>("font");

            // walls
            wallTopBot = Content.Load<Texture2D>("WallTopBot");
            wallRightLeft = Content.Load<Texture2D>("WallRightLeft");
            wallSidebump = Content.Load<Texture2D>("WallSidebump");






            for (int i = 0; i < 20; i++)
            {
                coins.Add(new Rectangle((int)coinPosition.X, (int)coinPosition.Y, 5, 5));
                coinPosition.X += 20;
            }

            for (int i = 0; i < 2; i++)
            {
                powers.Add(new Rectangle((int)powerPosition.X, (int)powerPosition.Y, 11, 11));
                powerPosition.X += 20;
            }


            void AddWalls()
            {
                walls.Add(new Rectangle(20, 500, testTexture.Width, testTexture.Height));
                walls.Add(new Rectangle(70, 550, testTexture.Width, testTexture.Height));

                //top and bottom
                walls.Add(new Rectangle(490, 65, wallTopBot.Width, wallTopBot.Height));
                walls.Add(new Rectangle(490, 825, wallTopBot.Width, wallTopBot.Height));
                //right and left
                walls.Add(new Rectangle(490, 65, wallRightLeft.Width, wallRightLeft.Height));
                walls.Add(new Rectangle(1100, 65, wallRightLeft.Width, wallRightLeft.Height));
                //bumps on the side
                walls.Add(new Rectangle(490, 335, wallSidebump.Width, wallSidebump.Height));
                walls.Add(new Rectangle(490, 465, wallSidebump.Width, wallSidebump.Height));
                walls.Add(new Rectangle(990, 335, wallSidebump.Width, wallSidebump.Height));
                walls.Add(new Rectangle(990, 465, wallSidebump.Width, wallSidebump.Height));
           

                walls.Add(new Rectangle(500, 75, 2, 2));





            }
            AddWalls();
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
        

        protected override void Update(GameTime gameTime)
        {
            pmBox = new Rectangle((int)pmPosition.X, (int)pmPosition.Y, 20, 20);

            gRedBox = new Rectangle((int)gRedPosition.X, (int)gRedPosition.Y, 18, 18);
            

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                pmVelocity.X = -pmSpeed;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                pmVelocity.X = pmSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                pmVelocity.Y = -pmSpeed;
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                pmVelocity.Y = pmSpeed;

            foreach (var wall in walls)
            {
                if (pmVelocity.X > 0 && TouchingLeft(wall) || pmVelocity.X < 0 && TouchingRight(wall))
                    pmVelocity.X = 0;
                if (pmVelocity.Y > 0 && TouchingTop(wall) || pmVelocity.Y < 0 && TouchingBottom(wall))
                    pmVelocity.Y = 0;
            }


            pmPosition += pmVelocity;
            pmVelocity = Vector2.Zero;

            int c = 0;
            foreach (var coin in coins)
            {
                if (TouchingLeft(coin) || TouchingRight(coin) || TouchingTop(coin) || TouchingBottom(coin))
                {
                    points++;
                    coins.RemoveAt(c);
                    break;
                }
                c++;
            }
            c = 0;


            int p = 0;
            foreach (var power in powers)
            {
                if (TouchingLeft(power) || TouchingRight(power) || TouchingTop(power) || TouchingBottom(power))
                {
                    powerTimer = 60 * 10;
                    powers.RemoveAt(p);
                    break;
                }
                p++;
            }
            p = 0;

            
            if(powerTimer > 0)
            {
                if (TouchingLeft(gRedBox) || TouchingRight(gRedBox) || TouchingTop(gRedBox) || TouchingBottom(gRedBox))
                {
                    gRedPosition.Y -= 50;
                }
            }

            else
            {
                if (TouchingLeft(gRedBox) || TouchingRight(gRedBox) || TouchingTop(gRedBox) || TouchingBottom(gRedBox))
                {
                    pmPosition.Y -= 100;
                }
            }


            if (powerTimer > 0)
            {
                powerTimer -= 1;
            }

/*
            if (coins.Count == 0)
            {
                pmPosition = new Vector2(50, 50);
                coins.Add(new Rectangle(-5, -5, 0, 0));
            }
*/





            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin();

            spriteBatch.Draw(pmTexture, pmPosition, Color.White);

            foreach (var wall in walls)
            {
                spriteBatch.Draw(wallSidebump, wall, Color.Blue);
            }

            foreach (var power in powers)
            {
                spriteBatch.Draw(powerTexture, power, Color.White);
            }

            foreach (var coin in coins)
            {
                spriteBatch.Draw(coinTexture, coin, Color.White);
            }

            spriteBatch.DrawString(font, points.ToString(), new Vector2(10, 20), Color.White);

            spriteBatch.DrawString(font, powerTimer.ToString(), new Vector2(30, 20), Color.White);

            spriteBatch.Draw(gRedTexture, gRedPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
