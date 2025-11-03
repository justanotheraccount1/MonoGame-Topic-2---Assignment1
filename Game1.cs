using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonoGame_Topic_2___Assignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D coinTexture, explosionTexture, bgTexture, startScreenTexture, buttonTexture;
        bool start = true;
        KeyboardState keyboardState;
        MouseState mouseState, previousMouseState;
        Rectangle window, buttonRect;
        List<Rectangle> coinRects = new List<Rectangle>();
        List<Rectangle> boomRects = new List<Rectangle>();
        List<float> fades = new List<float>();
        Random generator = new Random();
        SpriteFont scoreFont;
        int score = 0;
        List<Vector2> coinSpeeds = new List<Vector2>();


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            buttonRect = new Rectangle(350, 200, 100, 100);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            coinTexture = Content.Load<Texture2D>("Images/coin");
            bgTexture = Content.Load<Texture2D>("Images/Cyberspace");
            explosionTexture = Content.Load<Texture2D>("Images/8bitExplode");
            startScreenTexture = Content.Load<Texture2D>("Images/startScreen");
            buttonTexture = Content.Load<Texture2D>("Images/RedButton");
            scoreFont = Content.Load<SpriteFont>("Fonts/ScoreFont");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                start = false;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed)
            {
                if (buttonRect.Contains(mouseState.Position))
                {
                    coinRects.Add(new Rectangle(generator.Next(window.Width - 50), generator.Next(window.Height - 50), 50, 50));
                    coinSpeeds.Add(new Vector2(generator.Next(0, 10), generator.Next(0, 10)));
                }
                for (int i = 0; i < coinRects.Count; i++)
                {
                    if (coinRects[i].Contains(mouseState.Position))
                    {
                        boomRects.Add(coinRects[i]);
                        fades.Add(1f);
                        coinRects.RemoveAt(i);
                        coinSpeeds.RemoveAt(i);
                        i--;
                        score++;
                    }
                }
            }
            for (int i = 0; i < fades.Count; i++)
            {
                fades[i] -= 0.05f;
            }
            for (int i = 0; i < coinRects.Count; i++)
            {
                Rectangle temp;
                temp = coinRects[i];
                temp.X += (int)coinSpeeds[i].X;
                temp.Y += (int)coinSpeeds[i].Y;
                coinRects[i] = temp;
                if (coinRects[i].Top <= window.Top || coinRects[i].Bottom >= window.Bottom)
                {

                    Vector2 tempSpeed;
                    tempSpeed = coinSpeeds[i];
                    tempSpeed.Y *= -1;
                    coinSpeeds[i] = tempSpeed;
                }
                if (coinRects[i].Left <= window.Left || coinRects[i].Right >= window.Right)
                {
                    Vector2 tempSpeed;
                    tempSpeed = coinSpeeds[i];
                    tempSpeed.X *= -1;
                    coinSpeeds[i] = tempSpeed;
                }
            }
            // TODO: Add your update logic here
            
            previousMouseState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (start)
            {
                _spriteBatch.Draw(startScreenTexture, window, Color.White);
            }
            
            if (!start)
            {
                _spriteBatch.Draw(bgTexture, window, Color.White);
                _spriteBatch.DrawString(scoreFont, $"Score: {score}", new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(buttonTexture, buttonRect, Color.White);
                for (int i = 0; i < coinRects.Count; i++)
                {
                    _spriteBatch.Draw(coinTexture, coinRects[i], Color.White);
                }
                for (int i = 0; i < boomRects.Count; i++)
                {
                    _spriteBatch.Draw(explosionTexture, boomRects[i], Color.White * fades[i]);
                }

            }
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
