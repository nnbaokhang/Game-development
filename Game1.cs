using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
namespace Nguyen_Khang_lab3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
   

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        clsSprite mySprite1;
        clsSprite mySprite2;
        // Create a SoundEffect resource        
        // Create some sound resources
        SoundEffect soundEffect1;
        SoundEffect soundEffect2;
        SoundEffectInstance seInstance;
        // Since we will loop the music, we only want to play it once
        bool playMusic = true;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // changing the back buffer size changes the window size (in windowed mode)
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load the SoundEffect resource
            soundEffect1 = Content.Load<SoundEffect>("chord");
            soundEffect2 = Content.Load<SoundEffect>("music");
            // Create a SoundEffect instance that can be manipulated later
            seInstance = soundEffect1.CreateInstance();
            seInstance.IsLooped = true;


            // TODO: use this.Content to load your game content here
            mySprite1 = new clsSprite(Content.Load<Texture2D>("ball"),
            new Vector2(0f, 0f), new Vector2(64f, 64f),
             graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            
            mySprite2 = new clsSprite(Content.Load<Texture2D>("ball"),
            new Vector2(218f, 118f), new Vector2(64f, 64f),
            graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            
            // set the speed the sprites will move
            mySprite1.velocity = new Vector2(10, 10);
            //mySprite2.velocity = new Vector2(30, 30);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // Free the previously allocated resources
            mySprite1.texture.Dispose();
            spriteBatch.Dispose();

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mySprite1.Move();

            //mySprite2.Move();
            //Collision between balls detection
            if (mySprite1.CircleCollides(mySprite2))
            {
                mySprite1.velocity *= -1;
                GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
             
                    soundEffect1.Play();
                 
            }
            else
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            // Change the sprite 2 position using the left thumbstick of the Xbox controller
            // Vector2 LeftThumb = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
            // mySprite2.position += new Vector2(LeftThumb.X, -LeftThumb.Y) * 5;
            // Change the sprite 2 position using the keyboard
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
                mySprite2.position += new Vector2(0, -5);
            if (keyboardState.IsKeyDown(Keys.Down))
                mySprite2.position += new Vector2(0, 5);
            if (keyboardState.IsKeyDown(Keys.Left))
                mySprite2.position += new Vector2(-5, 0);
            if (keyboardState.IsKeyDown(Keys.Right))
                mySprite2.position += new Vector2(5, 0);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Draw the sprite using Alpha Blend, which uses transparency information if available
            spriteBatch.Begin();
            mySprite1.Draw(spriteBatch,Color.Blue);
            mySprite2.Draw(spriteBatch, Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
