using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
                _spriteBatch.Draw(buttonTexture, buttonRect, Color.White);
            }
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
