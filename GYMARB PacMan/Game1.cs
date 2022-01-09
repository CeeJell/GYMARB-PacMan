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
        Vector2 pmPosition = new Vector2(689, 342);
        Rectangle pmBox;
        float pmSpeed = 1.5f;
        Vector2 pmVelocity;

        Texture2D pmTest;

        Texture2D gRedTexture;
        Vector2 gRedPosition = new Vector2(692, 255);
        Rectangle gRedBox;

        Texture2D gBlueTexture;
        Vector2 gBluePosition = new Vector2(20, 100);
        Rectangle gBlueBox;

        Texture2D gPinkTexture;
        Vector2 gPinkPosition = new Vector2(40, 100);
        Rectangle gPinkBox;

        Texture2D gOrangeTexture;
        Vector2 gOrangePosition = new Vector2(60, 100);
        Rectangle gOrangeBox;

        Texture2D coinTexture;

        Texture2D powerTexture;
        double powerTimer = 0;

        Rectangle tpLeft;
        Rectangle tpRight;

        SpriteFont font;
        int points = 0;

        //walls
        Texture2D wallTopBot;
        Texture2D wallSideTop;
        Texture2D wallSideBottom;
        Texture2D wallSidebump;
        Texture2D wall15x60;
        Texture2D wall15x105;
        Texture2D wall30x15;
        Texture2D wall45x15;
        Texture2D wall45x30;
        Texture2D wall60x15;
        Texture2D wall60x30;
        Texture2D wall105x15;
        Texture2D wall135x15;

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


            // 405w x 450h 

            graphics.PreferredBackBufferWidth = 1405;
            graphics.PreferredBackBufferHeight = 650;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pmTexture = Content.Load<Texture2D>("Pacman");
            gRedTexture = Content.Load<Texture2D>("GhostRed");
            gBlueTexture = Content.Load<Texture2D>("GhostBlue");
            gPinkTexture = Content.Load<Texture2D>("GhostPink");
            gOrangeTexture = Content.Load<Texture2D>("GhostOrange");
            coinTexture = Content.Load<Texture2D>("Coin");
            powerTexture = Content.Load<Texture2D>("PowerUp");
            font = Content.Load<SpriteFont>("font");

            pmTest = Content.Load<Texture2D>("PacmanTest");

            // walls
            wallTopBot = Content.Load<Texture2D>("WallTopBot");
            wallSideTop = Content.Load<Texture2D>("WallSideTop");
            wallSideBottom = Content.Load<Texture2D>("WallSideBottom");
            wallSidebump = Content.Load<Texture2D>("WallSidebump");

            wall15x60 = Content.Load<Texture2D>("Wall15x60");
            wall15x105 = Content.Load<Texture2D>("Wall15x105");
            wall30x15 = Content.Load<Texture2D>("Wall30x15");
            wall45x15 = Content.Load<Texture2D>("Wall45x15");
            wall45x30 = Content.Load<Texture2D>("Wall45x30");
            wall60x15 = Content.Load<Texture2D>("Wall60x15");
            wall60x30 = Content.Load<Texture2D>("Wall60x30");
            wall105x15 = Content.Load<Texture2D>("Wall105x15");
            wall135x15 = Content.Load<Texture2D>("Wall135x15");






            void AddWalls()
            {

                //top and bottom
                walls.Add(new Rectangle(490, 90, wallTopBot.Width, wallTopBot.Height));
                walls.Add(new Rectangle(490, 550, wallTopBot.Width, wallTopBot.Height));
                //left and right
                walls.Add(new Rectangle(490, 90, wallSideTop.Width, wallSideTop.Height));
                walls.Add(new Rectangle(905, 90, wallSideTop.Width, wallSideTop.Height));
                walls.Add(new Rectangle(490, 325, wallSideBottom.Width, wallSideBottom.Height));
                walls.Add(new Rectangle(905, 325, wallSideBottom.Width, wallSideBottom.Height));
                //bumps on the side
                walls.Add(new Rectangle(500, 235, wallSidebump.Width, wallSidebump.Height));
                walls.Add(new Rectangle(500, 325, wallSidebump.Width, wallSidebump.Height));
                walls.Add(new Rectangle(830, 235, wallSidebump.Width, wallSidebump.Height));
                walls.Add(new Rectangle(830, 325, wallSidebump.Width, wallSidebump.Height));
                //walls inside
                walls.Add(new Rectangle(530, 130, wall45x30.Width, wall45x30.Height));
                walls.Add(new Rectangle(605, 130, wall60x30.Width, wall60x30.Height));
                walls.Add(new Rectangle(695, 100, wall15x60.Width, wall15x60.Height));
                walls.Add(new Rectangle(740, 130, wall60x30.Width, wall60x30.Height));
                walls.Add(new Rectangle(830, 130, wall45x30.Width, wall45x30.Height));

                walls.Add(new Rectangle(530, 190, wall45x15.Width, wall45x15.Height));
                walls.Add(new Rectangle(605, 190, wall15x105.Width, wall15x105.Height));
                walls.Add(new Rectangle(650, 190, wall105x15.Width, wall105x15.Height));
                walls.Add(new Rectangle(785, 190, wall15x105.Width, wall15x105.Height));
                walls.Add(new Rectangle(830, 190, wall45x15.Width, wall45x15.Height));

                walls.Add(new Rectangle(605, 235, wall60x15.Width, wall60x15.Height));
                walls.Add(new Rectangle(695, 190, wall15x60.Width, wall15x60.Height));
                walls.Add(new Rectangle(740, 235, wall60x15.Width, wall60x15.Height));

                walls.Add(new Rectangle(605, 325, wall15x60.Width, wall15x60.Height));
                walls.Add(new Rectangle(650, 370, wall105x15.Width, wall105x15.Height));
                walls.Add(new Rectangle(695, 370, wall15x60.Width, wall15x60.Height));
                walls.Add(new Rectangle(785, 325, wall15x60.Width, wall15x60.Height));

                walls.Add(new Rectangle(530, 415, wall45x15.Width, wall45x15.Height));
                walls.Add(new Rectangle(560, 415, wall15x60.Width, wall15x60.Height));
                walls.Add(new Rectangle(605, 415, wall60x15.Width, wall60x15.Height));
                walls.Add(new Rectangle(740, 415, wall60x15.Width, wall60x15.Height));
                walls.Add(new Rectangle(830, 415, wall15x60.Width, wall15x60.Height));
                walls.Add(new Rectangle(830, 415, wall45x15.Width, wall45x15.Height));

                walls.Add(new Rectangle(500, 460, wall30x15.Width, wall30x15.Height));
                walls.Add(new Rectangle(650, 460, wall105x15.Width, wall105x15.Height));
                walls.Add(new Rectangle(875, 460, wall30x15.Width, wall30x15.Height));

                walls.Add(new Rectangle(605, 460, wall15x60.Width, wall15x60.Height));
                walls.Add(new Rectangle(695, 460, wall15x60.Width, wall15x60.Height));
                walls.Add(new Rectangle(785, 460, wall15x60.Width, wall15x60.Height));

                walls.Add(new Rectangle(530, 505, wall135x15.Width, wall135x15.Height));
                walls.Add(new Rectangle(740, 505, wall135x15.Width, wall135x15.Height));

                //spökbox (inte klar)
                walls.Add(new Rectangle(650, 280, 105, wallSidebump.Height));
            }
            AddWalls();

            void AddCoins()
            {
                // coins
                int coinX;
                int coinY;

                coinX = 513;
                for (int i = 0; i < 12; i++)
                {
                    coins.Add(new Rectangle(coinX, 113, 5, 5));
                    coinX += 15;
                }

                coinX = 723;
                for (int i = 0; i < 12; i++)
                {
                    coins.Add(new Rectangle(coinX, 113, 5, 5));
                    coinX += 15;
                }

                coinX = 513;
                for (int i = 0; i < 26; i++)
                {
                    coins.Add(new Rectangle(coinX, 173, 5, 5));
                    coinX += 15;
                }

                coinX = 513;
                for (int i = 0; i < 6; i++)
                {
                    coins.Add(new Rectangle(coinX, 218, 5, 5));
                    coinX += 15;
                }

                coinX = 633;
                for (int i = 0; i < 4; i++)
                {
                    coins.Add(new Rectangle(coinX, 218, 5, 5));
                    coinX += 15;
                }

                coinX = 723;
                for (int i = 0; i < 4; i++)
                {
                    coins.Add(new Rectangle(coinX, 218, 5, 5));
                    coinX += 15;
                }

                coinX = 813;
                for (int i = 0; i < 6; i++)
                {
                    coins.Add(new Rectangle(coinX, 218, 5, 5));
                    coinX += 15;
                }

                coinX = 513;
                for (int i = 0; i < 12; i++)
                {
                    coins.Add(new Rectangle(coinX, 398, 5, 5));
                    coinX += 15;
                }

                coinX = 723;
                for (int i = 0; i < 12; i++)
                {
                    coins.Add(new Rectangle(coinX, 398, 5, 5));
                    coinX += 15;
                }

                coinX = 528;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(coinX, 443, 5, 5));
                    coinX += 15;
                }

                coinX = 588;
                for (int i = 0; i < 16; i++)
                {
                    coins.Add(new Rectangle(coinX, 443, 5, 5));
                    coinX += 15;
                }

                coinX = 858;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(coinX, 443, 5, 5));
                    coinX += 15;
                }

                coinX = 513;
                for (int i = 0; i < 6; i++)
                {
                    coins.Add(new Rectangle(coinX, 488, 5, 5));
                    coinX += 15;
                }

                coinX = 633;
                for (int i = 0; i < 4; i++)
                {
                    coins.Add(new Rectangle(coinX, 488, 5, 5));
                    coinX += 15;
                }

                coinX = 723;
                for (int i = 0; i < 4; i++)
                {
                    coins.Add(new Rectangle(coinX, 488, 5, 5));
                    coinX += 15;
                }

                coinX = 813;
                for (int i = 0; i < 6; i++)
                {
                    coins.Add(new Rectangle(coinX, 488, 5, 5));
                    coinX += 15;
                }

                coinX = 513;
                for (int i = 0; i < 26; i++)
                {
                    coins.Add(new Rectangle(coinX, 533, 5, 5));
                    coinX += 15;
                }






                coinY = 128;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(513, coinY, 5, 5));
                    coinY += 30;
                }

                coinY = 128;
                for (int i = 0; i < 3; i++)
                {
                    coins.Add(new Rectangle(588, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 128;
                for (int i = 0; i < 3; i++)
                {
                    coins.Add(new Rectangle(678, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 128;
                for (int i = 0; i < 3; i++)
                {
                    coins.Add(new Rectangle(723, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 128;
                for (int i = 0; i < 3; i++)
                {
                    coins.Add(new Rectangle(813, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 128;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(888, coinY, 5, 5));
                    coinY += 30;
                }

                coinY = 173;
                for (int i = 0; i < 3; i++)
                {
                    coins.Add(new Rectangle(513, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 173;
                for (int i = 0; i < 3; i++)
                {
                    coins.Add(new Rectangle(588, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 188;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(633, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 188;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(768, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 173;
                for (int i = 0; i < 3; i++)
                {
                    coins.Add(new Rectangle(813, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 173;
                for (int i = 0; i < 3; i++)
                {
                    coins.Add(new Rectangle(888, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 233;
                for (int i = 0; i < 11; i++)
                {
                    coins.Add(new Rectangle(588, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 233;
                for (int i = 0; i < 11; i++)
                {
                    coins.Add(new Rectangle(813, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 413;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(513, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 413;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(588, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 413;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(678, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 413;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(723, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 413;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(813, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 413;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(888, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 458;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(543, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 458;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(588, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 458;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(633, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 458;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(768, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 458;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(813, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 458;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(858, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 503;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(513, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 503;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(678, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 503;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(723, coinY, 5, 5));
                    coinY += 15;
                }

                coinY = 503;
                for (int i = 0; i < 2; i++)
                {
                    coins.Add(new Rectangle(888, coinY, 5, 5));
                    coinY += 15;
                }
            }
            AddCoins();


            powers.Add(new Rectangle(509, 139, 12, 12));
            powers.Add(new Rectangle(884, 139, 12, 12));
            powers.Add(new Rectangle(509, 439, 12, 12));
            powers.Add(new Rectangle(884, 439, 12, 12));


            tpLeft = new Rectangle(480, 290, 1, 20);
            tpRight = new Rectangle(925, 290, 1, 20);


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
            pmBox = new Rectangle((int)pmPosition.X, (int)pmPosition.Y, 27, 27);

            gRedBox = new Rectangle((int)gRedPosition.X, (int)gRedPosition.Y, gRedTexture.Width, gRedTexture.Height);

            gBlueBox = new Rectangle((int)gBluePosition.X, (int)gBluePosition.Y, gBlueTexture.Width, gBlueTexture.Height);

            gPinkBox = new Rectangle((int)gPinkPosition.X, (int)gPinkPosition.Y, gPinkTexture.Width, gPinkTexture.Height);

            gOrangeBox = new Rectangle((int)gOrangePosition.X, (int)gOrangePosition.Y, gOrangeTexture.Width, gOrangeTexture.Height);


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

            if (TouchingRight(tpLeft))
            {
                pmPosition.X += 420;
            }

            if (TouchingLeft(tpRight))
            {
                pmPosition.X -= 420;
            }


            if(powerTimer > 0)
            {
                if (TouchingLeft(gRedBox) || TouchingRight(gRedBox) || TouchingTop(gRedBox) || TouchingBottom(gRedBox))
                {
                    gRedPosition.Y -= 50;
                }
                if (TouchingLeft(gBlueBox) || TouchingRight(gBlueBox) || TouchingTop(gBlueBox) || TouchingBottom(gBlueBox))
                {
                    gBluePosition.Y -= 50;
                }
                if (TouchingLeft(gPinkBox) || TouchingRight(gPinkBox) || TouchingTop(gPinkBox) || TouchingBottom(gPinkBox))
                {
                    gPinkPosition.Y -= 50;
                }
                if (TouchingLeft(gOrangeBox) || TouchingRight(gOrangeBox) || TouchingTop(gOrangeBox) || TouchingBottom(gOrangeBox))
                {
                    gOrangePosition.Y -= 50;
                }
                powerTimer -= 1;
            }

            else
            {
                if (TouchingLeft(gRedBox) || TouchingRight(gRedBox) || TouchingTop(gRedBox) || TouchingBottom(gRedBox))
                {
                    pmPosition.Y -= 100;
                }
                if (TouchingLeft(gBlueBox) || TouchingRight(gBlueBox) || TouchingTop(gBlueBox) || TouchingBottom(gBlueBox))
                {
                    pmPosition.Y -= 100;
                }
                if (TouchingLeft(gPinkBox) || TouchingRight(gPinkBox) || TouchingTop(gPinkBox) || TouchingBottom(gPinkBox))
                {
                    pmPosition.Y -= 100;
                }
                if (TouchingLeft(gOrangeBox) || TouchingRight(gOrangeBox) || TouchingTop(gOrangeBox) || TouchingBottom(gOrangeBox))
                {
                    pmPosition.Y -= 100;
                }
            }



            if (coins.Count == 0)
            {
                pmPosition = new Vector2(50, 50);
                coins.Add(new Rectangle(-5, -5, 0, 0));
            }





            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin();

            spriteBatch.Draw(pmTexture, pmPosition, Color.White);

            foreach (var wall in walls)
            {
                spriteBatch.Draw(wall135x15, wall, Color.Blue);
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

            spriteBatch.DrawString(font, powerTimer.ToString(), new Vector2(40, 20), Color.White);

            spriteBatch.Draw(gBlueTexture, gBluePosition, Color.White);

            spriteBatch.Draw(gPinkTexture, gPinkPosition, Color.White);

            spriteBatch.Draw(gOrangeTexture, gOrangePosition, Color.White);

            spriteBatch.Draw(gRedTexture, gRedPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
