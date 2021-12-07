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
        /*
        bool Hit;
        char pmDirection;
        bool DisableW;
        bool DisableA;
        bool DisableS;
        bool DisableD;
        */

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            pmPosition = new Vector2(300, 200);




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

            // TODO: use this.Content to load your game content here
        }

        /*
        protected void Disable()
        {
            DisableW = true;
            DisableA = true;
            DisableS = true;
            DisableD = true;
        }
        

        protected void ifHit(char direction)
        {
            if (direction == 'W')
            {
                Disable();
                pmPosition = new Vector2(pmPosition.X, pmPosition.Y + 10f);
                DisableA = false;
                DisableS = false;
                DisableD = false;
            }
            if (direction == 'A')
            {
                Disable();
                pmPosition = new Vector2(pmPosition.X + 10f, pmPosition.Y);
                DisableW = false;
                DisableS = false;
                DisableD = false;
            }
            if (direction == 'S')
            {
                Disable();
                pmPosition = new Vector2(pmPosition.X, pmPosition.Y - 10f);
                DisableW = false;
                DisableA = false;
                DisableD = false;
            }
            if (direction == 'D')
            {
                Disable();
                pmPosition = new Vector2(pmPosition.X - 10f, pmPosition.Y);
                DisableW = false;
                DisableA = false;
                DisableS = false;
            }
        }

        */

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            // Pacmans rörelse

            pmPosition += pmSpeed;



            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W))
            {
                pmSpeed = new Vector2(0, -2.0f);

            }
            else if (state.IsKeyDown(Keys.D))
            {
                pmSpeed = new Vector2(2.0f, 0);

            }
            else if (state.IsKeyDown(Keys.A))
            {
                pmSpeed = new Vector2(-2.0f, 0);

            }
            else if (state.IsKeyDown(Keys.S))
            {
                pmSpeed = new Vector2(0, 2.0f);

            }
            else
            {
                pmSpeed = new Vector2(0, 0);
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(pmTexture, pmPosition, Color.White);
            spriteBatch.Draw(test, new Vector2 (200,200), Color.White);
            spriteBatch.End();






            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }










      
    }
}
