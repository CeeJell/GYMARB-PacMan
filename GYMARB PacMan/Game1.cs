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
        Vector2 pmSpeed;
        Texture2D test;

        private List<Sprite> _sprites;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {





            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            base.Initialize();
        }




        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pmTexture = Content.Load<Texture2D>("Pacman");
            test = Content.Load<Texture2D>("Test");

            _sprites = new List<Sprite>()
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
                    Colour = Color.Yellow,
                    Speed = 3f,
                },
                new Player(test)
                {
                    Input = new Models.Input()
                    {
                        Left = Keys.Left,
                        Right = Keys.Right,
                        Up = Keys.Up,
                        Down = Keys.Down,
                    },
                    Position = new Vector2(300, 300),
                    Colour = Color.Red,
                    Speed = 3f,
                },
            };
        }

        

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            base.Update(gameTime);


        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (var sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();






            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }










      
    }
}
