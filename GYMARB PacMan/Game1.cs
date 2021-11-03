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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            pmPosition = new Vector2(300, 200);




            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pmTexture = Content.Load<Texture2D>("Pacman");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(pmTexture, pmPosition, Color.White);
            spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
