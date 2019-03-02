using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
namespace PingPong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>


    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        clsSprite ball;
        //Paddle play
        paddle computer;
        paddle human;
        // Create a SoundEffect resource        
        // Create some sound resources
        SoundEffect ballHit;
        SoundEffect killShot;
        SoundEffect miss;
        SoundEffectInstance seInstance;
        //Score
        public int scorePlayer = 0;
        public int scoreComputer = 0;
        //Font 
        SpriteFont Font1;
        Vector2 FontPos;
        public string victory; // used to hold the congratulations message
        bool done = false;
        int speed = 0; //Increasing speed 
        float dt = 1f;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // changing the back buffer size changes the window size (in windowed mode)
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
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
            ballHit = Content.Load<SoundEffect>("ballhit");
            killShot = Content.Load<SoundEffect>("killshot");

            // Create a SoundEffect instance that can be manipulated later
            seInstance = ballHit.CreateInstance();
            // seInstance.IsLooped = true;
            //Set font
            Font1 = Content.Load<SpriteFont>("Courier New");
            // TODO: use this.Content to load your game content here
            ball = new clsSprite(Content.Load<Texture2D>("small_ball"),
            new Vector2(graphics.PreferredBackBufferHeight / 2, graphics.PreferredBackBufferWidth / 2), new Vector2(35f, 35f),
             graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            //Paddle for computer
            //Left hand side , position (0,height/2), size(10,height)
            computer = new paddle(Content.Load<Texture2D>("left_paddle"),
            new Vector2(50f, graphics.PreferredBackBufferHeight / 2), new Vector2(50f, 133f),
             graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            //Paddle for human
            //Righ hand side, position( width , height/2), size(10,height);
            human = new paddle(Content.Load<Texture2D>("right_paddle"),
            new Vector2(graphics.PreferredBackBufferWidth - 100f, graphics.PreferredBackBufferHeight / 2), new Vector2(50f, 133f),
             graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            // set the speed the sprites will move
            //Set random number between 0 and 10
            ball.velocity = new Vector2(10.1f, 10.1f);
            computer.velocity = new Vector2(0, 5.1f);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // Free the previously allocated resources
            ball.texture.Dispose();
            human.texture.Dispose();
            computer.texture.Dispose();
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
            ball.Move();
            computer.Move(ball);
            //mySprite2.Move();
            //Collision between ball and right paddle
            //1 mean go from top to bottom
            if (ball.Player_Collide(human) == 1 || ball.Computer_Collide(computer) == 1)
            {
                ball.velocity = new Vector2(-ball.velocity.X, ball.velocity.Y);
            }
            //2 mean go from bottom to top
            else if (ball.Player_Collide(human) == 2 || ball.Computer_Collide(computer) == 2)
            {
                ball.velocity = new Vector2(-ball.velocity.X, ball.velocity.Y);
            }
           
            //Score
            if (ball.center.X <= 0)
            {
                scorePlayer++;
            }
             else if(ball.position.X + ball.size.Y >= graphics.PreferredBackBufferWidth) 
            {
                scoreComputer++;
            }
            // Change the sprite 2 position using the left thumbstick of the Xbox controller
            // Vector2 LeftThumb = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
            // mySprite2.position += new Vector2(LeftThumb.X, -LeftThumb.Y) * 5;
            // Change the sprite 2 position using the keyboard
            KeyboardState keyboardState = Keyboard.GetState();
            //Human paddle can only move between 0 and screenSize
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (human.position.Y + human.velocity.Y >= 0)
                    human.position += new Vector2(0, -7.1f);
               
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if(human.position.Y + human.size.Y/1.75 <= graphics.PreferredBackBufferHeight)
                human.position += new Vector2(0, 7.1f);
                
            }

            //Victory
            if (Math.Abs(scoreComputer - scorePlayer) == 5)
            {
                victory = "Congratulations!You Win!Your Score: " + scorePlayer + " Computer Score: " + scoreComputer;
                done = true;
              
            }
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
            ball.Draw(spriteBatch);
            human.Draw(spriteBatch);
            computer.Draw(spriteBatch);
            // Draw running score string
            spriteBatch.DrawString(Font1, "Computer: " + scoreComputer, new Vector2(5, 10),
            Color.Yellow);
            spriteBatch.DrawString(Font1, "Player: " + scorePlayer,
            new Vector2(graphics.GraphicsDevice.Viewport.Width - Font1.MeasureString("Player: " +
            ball.scorePlayer).X - 30, 10), Color.Yellow);
            if (done) //draw victory/consolation message
            {
                FontPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 300,
                (graphics.GraphicsDevice.Viewport.Height / 2) - 50);
                spriteBatch.DrawString(Font1, victory, FontPos, Color.Yellow);
            }
            //Draw the other sprites
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
