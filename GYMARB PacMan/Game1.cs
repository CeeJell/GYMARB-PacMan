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
        bool Hit;
        char pmDirection;
        bool DisableW;
        bool DisableA;
        bool DisableS;
        bool DisableD;

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


            test = Content.Load<Texture2D>("Test");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            // Pacmans rörelse

            pmPosition += pmSpeed;

            if (Hit)
            {
                if (pmDirection == 'W')
                {
                    DisableW = true;
                    pmPosition = new Vector2(pmPosition.X, pmPosition.Y + 1f);
                }
                if (pmDirection == 'A')
                {
                    DisableA = true;
                    pmPosition = new Vector2(pmPosition.X + 1f, pmPosition.Y);
                }
                if (pmDirection == 'S')
                {
                    DisableS = true;
                    pmPosition = new Vector2(pmPosition.X, pmPosition.Y - 1f);
                }
                if (pmDirection == 'D')
                {
                   DisableD = true;
                   pmPosition = new Vector2(pmPosition.X - 1f, pmPosition.Y);
                }
            }

            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W) && !DisableW)
            {
                pmSpeed = new Vector2(0, -2.0f);
                pmDirection = 'W';
                if (DisableS) DisableS = false;
      //          if (DisableD) DisableD = false;
      //          if (DisableA) DisableA = false;
            }
            else if (state.IsKeyDown(Keys.D) && !DisableD)
            {
                pmSpeed = new Vector2(2.0f, 0);
                pmDirection = 'D';
                if (DisableA) DisableA = false;
       //         if (DisableS) DisableS = false;
       //         if (DisableW) DisableW = false;
            }
            else if (state.IsKeyDown(Keys.A) && !DisableA)
            {
                pmSpeed = new Vector2(-2.0f, 0);
                pmDirection = 'A';
                if (DisableD) DisableD = false;
     //           if (DisableS) DisableS = false;
     //           if (DisableW) DisableW = false;
            }
            else if (state.IsKeyDown(Keys.S) && !DisableS)
            {
                pmSpeed = new Vector2(0, 2.0f);
                pmDirection = 'S';
                if (DisableW) DisableW = false;
      //          if (DisableD) DisableD = false;
      //          if (DisableA) DisableA = false;
            }
            else
            {
                pmSpeed = new Vector2(0, 0);
            }


            //kollision

            Rectangle pmBox = new Rectangle((int)pmPosition.X, (int)pmPosition.Y, pmTexture.Width, pmTexture.Height);
            Rectangle testBox = new Rectangle((int)200, (int)200, 512, 512);

            var kollision = Intersection(pmBox, testBox);

            if (kollision.Width > 0 && kollision.Height > 0)
            {
                Rectangle r1 = Normalize(pmBox, kollision);
                Rectangle r2 = Normalize(testBox, kollision);
                Hit = TestCollision(pmTexture, r1, test, r2);

            }



            base.Update(gameTime);
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
        public static Rectangle Normalize(Rectangle reference, Rectangle overlap)
        {
            //Räkna ut en rektangel som kan användas relativt till referensrektangeln
            return new Rectangle(
              overlap.X - reference.X,
              overlap.Y - reference.Y,
              overlap.Width,
              overlap.Height);
        }
        public static bool TestCollision(Texture2D t1, Rectangle r1, Texture2D t2, Rectangle r2)
        {
            //Beräkna hur många pixlar som finns i området som ska undersökas
            int pixelCount = r1.Width * r1.Height;
            uint[] texture1Pixels = new uint[pixelCount];
            uint[] texture2Pixels = new uint[pixelCount];

            //Kopiera ut pixlarna från båda områdena
            t1.GetData(0, r1, texture1Pixels, 0, pixelCount);
            t2.GetData(0, r2, texture2Pixels, 0, pixelCount);

            //Jämför om vi har några pixlar som överlappar varandra i områdena
            for (int i = 0; i < pixelCount; ++i)
            {
                if (((texture1Pixels[i] & 0xff000000) > 0) && ((texture2Pixels[i] & 0xff000000) > 0))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
