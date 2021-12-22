using GYMARB_PacMan.Models.Sprites;
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
        Vector2 pmPosition;
        Texture2D coinTexture;
        Vector2 coinPosition = new Vector2(200, 20);
        public Rectangle coinBox;

        Texture2D test;

        SpriteFont font;
        int points = 0;

        private List<Wall> _walls;
        private List<Vector2> coins;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            coins = new List<Vector2>();



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
            test = Content.Load<Texture2D>("Test");
            font = Content.Load<SpriteFont>("font");

            _walls = new List<Wall>()
            {
                new Player(pmTexture)
                {
                    Input = new Models.Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                        Up = Keys.W,
                        Down = Keys.S,
                    },
                    Position = new Vector2(100, 100),
                    Colour = Color.White,
                    Speed = 3f,
                },
                new Player(test)
                {
                    Input = new Models.Input()
                    {
                        Left = Keys.O,
                        Right = Keys.O,
                        Up = Keys.O,
                        Down = Keys.O,
                    },
                    Position = new Vector2(300, 300),
                    Colour = Color.Red,
                    Speed = 0f,
                },
                new Player(test)
                {
                    Input = new Models.Input()
                    {
                        Left = Keys.O,
                        Right = Keys.O,
                        Up = Keys.O,
                        Down = Keys.O,
                    },
                    Position = new Vector2(400, 200),
                    Colour = Color.Red,
                    Speed = 0f,
                },
            };

            coins.Add(new Vector2(coinPosition.X, coinPosition.Y));
            coinBox = new Rectangle((int)coinPosition.X, (int)coinPosition.Y, 5, 5);

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            foreach (var sprite in _walls)
                sprite.Update(gameTime, _walls);

            

            base.Update(gameTime);





        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            foreach (var coin in coins)
            {
                spriteBatch.Draw(coinTexture, coin, Color.White);
            }

            spriteBatch.DrawString(font, points.ToString(), new Vector2(10, 20), Color.White);

            foreach (var wall in _walls)
            {
                wall.Draw(spriteBatch);
            }
            spriteBatch.End();






            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

      
    }
}
