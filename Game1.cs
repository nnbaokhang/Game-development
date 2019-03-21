using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

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
        private Texture2D background;
        
        clsSprite ball;
        //Paddle play
        paddle computer;
        paddle human;
        paddle rightComputer;
        // Create a SoundEffect resource        
        // Create some sound resources
        SoundEffect ballHit;
        SoundEffect killShot;
        SoundEffect miss;
        SoundEffect shotGunMenu;
        Song song;
        //Score
        public int scorePlayer = 0;
        public int scoreComputer = 0;
        //Font 
        SpriteFont Font1;
        Vector2 FontPos;
        public string victory; // used to hold the congratulations message
        bool done = false;
        private Texture2D menu;
        private Texture2D playMenu;
        private Texture2D computerMenu;
        private Texture2D creditMenu;
        private Texture2D exitMenu;
        private Texture2D hoverPlay;
        private Texture2D hoverDemo;
        private Texture2D hoverCredit;
        private Texture2D hoverExit;
        //Computer vs player
        //Click
        bool isClickedPlayGame = false;
        bool isClickedMusic = false;
        bool isClickedDemoGame = false;
        bool isClickedCredit = false;
        bool isClickExit = false;
        //Hover
        bool isHoverPlayer = false;
        bool isHoverComputer = false;
        bool isHoverExit = false;
        bool isHoverCredit = false;
        bool isBackgroundSound = true;
        //Game over
        bool gameOver = false;
        float dt = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // changing the back buffer size changes the window size (in windowed mode)
            graphics.PreferredBackBufferWidth = 1920;//1920
            graphics.PreferredBackBufferHeight = 1080;//1080
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
            //Load menu
            menu = Content.Load<Texture2D>("Ping_Pong_Menu");
            //gamePlayMenu = Content.Load<Texture2D>("play-menu");
            playMenu = Content.Load<Texture2D>("menu-play");
            computerMenu= Content.Load<Texture2D>("menu-computer");
            creditMenu = Content.Load<Texture2D>("menu-credits");
            exitMenu = Content.Load<Texture2D>("menu-exit");
            hoverPlay = Content.Load<Texture2D>("hover-play");
            hoverDemo = Content.Load<Texture2D>("hover-computer");
            hoverCredit = Content.Load<Texture2D>("hover-credit");
            hoverExit = Content.Load<Texture2D>("hover-exit");
            //End load menu
            //Load background
            background = Content.Load<Texture2D>("table");
            // Load the SoundEffect resource
            ballHit = Content.Load<SoundEffect>("ballhit");
            killShot = Content.Load<SoundEffect>("killshot");
            miss = Content.Load<SoundEffect>("miss");
            shotGunMenu = Content.Load<SoundEffect>("shotgun");
            song = Content.Load<Song>("backgroundMusic");
            
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 10f;
           
            // Create a SoundEffect instance that can be manipulated later
            // seInstance.IsLooped = true;
            //Set font
            Font1 = Content.Load<SpriteFont>("Courier New");
            // TODO: use this.Content to load your game content here
            //Draw ball
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

            rightComputer = new paddle(Content.Load<Texture2D>("right_paddle"),
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
        protected void soundState()
        {
            // Sound state
            if (!isClickedDemoGame && !isClickedPlayGame)
            {
                isBackgroundSound = true;
            }
            else
            {
                isBackgroundSound = false;
            }
            if (isBackgroundSound)
            {
                MediaPlayer.Volume = 10f;
            }
            else
            {
                MediaPlayer.Volume = 0f;
            }
        }
        void hoverState(MouseState mouseState)
        {
            //Hover state
            if (mouseState.X >= 888 && mouseState.X <= 1108 && mouseState.Y >= 570 && mouseState.Y <= 630)
            {
                isHoverPlayer = true;

            }
            else if (mouseState.X >= 770 && mouseState.X <= 1250 && mouseState.Y >= 651 && mouseState.Y <= 705)
            {
                isHoverComputer = true;
            }
            else if (mouseState.X >= 775 && mouseState.X <= 1200 && mouseState.Y >= 723 && mouseState.Y <= 770)
            {
                isHoverCredit = true;
            }
            else if (mouseState.X >= 750 && mouseState.X <= 1250 && mouseState.Y >= 830 && mouseState.Y <= 900)
            {
                isHoverExit = true;
            }
            else
            {
                isHoverPlayer = false;
                isHoverComputer = false;
                isHoverExit = false;
                isHoverCredit = false;
            }
        }
        void clickState(MouseState mouseState)
        {
            //Click state
            if (mouseState.LeftButton == ButtonState.Pressed && mouseState.X >= 888 && mouseState.X <= 1108 && mouseState.Y >= 570 && mouseState.Y <= 630)
            {
                isClickedPlayGame = true;
                isClickedMusic = true;
                done = false;

            }
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseState.X >= 770 && mouseState.X <= 1250 && mouseState.Y >= 651 && mouseState.Y <= 705)
            {
                isClickedDemoGame = true;
                isClickedMusic = true;
                done = false;
            }
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseState.X >= 775 && mouseState.X <= 1200 && mouseState.Y >= 723 && mouseState.Y <= 770)
            {
                isClickedMusic = true;
                isClickedCredit = true;
            }
            else if (mouseState.LeftButton == ButtonState.Pressed && mouseState.X >= 750 && mouseState.X <= 1250 && mouseState.Y >= 830 && mouseState.Y <= 900)
            {
                isClickExit = true;
            }
        }
        void playGame(GameTime gameTime)
        {
            //Game play mode
          
            {
                if (isClickedMusic)
                    shotGunMenu.Play();
                isClickedMusic = false;
                //Hit escape then exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    isClickedPlayGame = false;
                    gameOver = false;
                    done = false;
                    scoreComputer = 0;
                    scorePlayer = 0;
                }
                //Time for calculate velocity
                if (!gameOver)
                {
                    dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 10f;

                    //Play right here
                    // TODO: Add your update logic here
                    ball.Move(dt);
                    computer.Move(ball);

                    //Check if hit upper part and not in the surface
                    if (ball.Player_Collide(human) == -1)
                    {
                        ball.velocity = new Vector2(10, -10);
                    }
                    //Check if hit lower part and not in the surface
                    else if (ball.Player_Collide(human) == -2)
                    {
                        ball.velocity = new Vector2(10, 10);
                    }
                    //upper part
                    else if (ball.Player_Collide(human) == 1 || ball.Computer_Collide(computer) == 1)
                    {
                        ballHit.Play();
                        ball.velocity = new Vector2(-ball.velocity.X, ball.velocity.Y);
                    }
                    //lower part
                    else if (ball.Player_Collide(human) == 2 || ball.Computer_Collide(computer) == 2)
                    {
                        ballHit.Play();
                        ball.velocity = new Vector2(-ball.velocity.X, ball.velocity.Y);
                    }
                    //Dead hit upper part
                    else if (ball.Player_Collide(human) == 3)
                    {
                        ballHit.Play();
                        ball.velocity = new Vector2(-1.05f * ball.velocity.X, ball.velocity.Y);
                    }
                    //Dead hit lower part
                    else if (ball.Player_Collide(human) == 4)
                    {
                        ballHit.Play();
                        ball.velocity = new Vector2(-1.05f * ball.velocity.X, ball.velocity.Y);
                    }
                    //Doesn't matter
                    else if (ball.Player_Collide(human) == 0)
                    {

                        ball.velocity = new Vector2(ball.velocity.X, ball.velocity.Y);
                    }

                    //Score
                    if (ball.position.X + ball.velocity.X <= 0)
                    {
                        killShot.Play();
                        scorePlayer++;
                    }
                    //Paddle miss ball
                    else if (ball.position.X + ball.size.X >= graphics.PreferredBackBufferWidth)
                    {
                        miss.Play();
                        scoreComputer++;
                    }

                    // Change the right paddle position using the keyboard
                    KeyboardState keyboardState = Keyboard.GetState();
                    //Human paddle can only move between 0 and screenSize
                    if (keyboardState.IsKeyDown(Keys.Up))
                    {
                        if (human.position.Y + human.velocity.Y >= 0)
                            human.position += new Vector2(0, -7.1f);

                    }
                    if (keyboardState.IsKeyDown(Keys.Down))
                    {
                        if (human.position.Y + human.size.Y / 0.9 <= graphics.PreferredBackBufferHeight)
                            human.position += new Vector2(0, 7.1f);

                    }
                }
                //Victory
                if (scorePlayer - scoreComputer == 8)
                {

                    victory = "Congratulations!You Win!Your Score: " + scorePlayer + " Computer Score: " + scoreComputer;
                    done = true;
                    gameOver = true;

                }
                //Lose
                else if (scoreComputer - scorePlayer == 8)
                {
                    victory = "Ooops!You Lose!Your Score: " + scorePlayer + " Computer Score: " + scoreComputer;
                    done = true;
                    gameOver = true;
                }
            }
        }

        //Game Demo mode
        void demoMode(GameTime gameTime)
        { 
            
            
         
            {
                if (isClickedMusic)
                    shotGunMenu.Play();
                isClickedMusic = false;
                //Hit escape then exit play mode
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    isClickedPlayGame = false;
                    gameOver = false;
                    isClickedDemoGame = false;
                    scoreComputer = 0;
                    scorePlayer = 0;
                }
                //Time for calculate velocity
                if (!gameOver)
                {
                    dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 10f;

                    //Play right here
                    // TODO: Add your update logic here
                    ball.Move(dt);
                    computer.Move(ball);
                    rightComputer.MoveRight(ball);
                    //mySprite2.Move();
                    //Collision between ball and right paddle
                    //upper part
                    if (ball.Player_Collide(rightComputer) == 1 || ball.Computer_Collide(computer) == 1)
                    {
                        ballHit.Play();
                        ball.velocity = new Vector2(-ball.velocity.X, ball.velocity.Y);
                    }
                    //lower part
                    else if (ball.Player_Collide(rightComputer) == 2 || ball.Computer_Collide(computer) == 2)
                    {
                        ballHit.Play();
                        ball.velocity = new Vector2(-ball.velocity.X, ball.velocity.Y);
                    }
                    //Dead hit upper part
                    else if (ball.Player_Collide(rightComputer) == 3)
                    {
                        ballHit.Play();
                        ball.velocity = new Vector2(-1.05f * ball.velocity.X, ball.velocity.Y);
                    }
                    //Dead hit lower part
                    else if (ball.Player_Collide(rightComputer) == 4)
                    {
                        ballHit.Play();
                        ball.velocity = new Vector2(-1.05f * ball.velocity.X, ball.velocity.Y);
                    }
                    //Score
                    if (ball.position.X + ball.velocity.X <= 0)
                    {
                        killShot.Play();
                        scorePlayer++;
                    }
                    else if (ball.position.X + ball.size.Y >= graphics.PreferredBackBufferWidth)
                    {
                        miss.Play();
                        scoreComputer++;
                    }
                    // Change the sprite 2 position using the left thumbstick of the Xbox controller
                    // Vector2 LeftThumb = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
                    // mySprite2.position += new Vector2(LeftThumb.X, -LeftThumb.Y) * 5;
                    // Change the sprite 2 position using the keyboard

                }
                //Victory
                if (scorePlayer - scoreComputer == 8)
                {

                    victory = "Congratulations!You Win!Your Score: " + scorePlayer + " Computer Score: " + scoreComputer;
                    done = true;
                    gameOver = true;

                }
                else if (scoreComputer - scorePlayer == 8)
                {
                    victory = "Ooops!You Lose!Your Score: " + scorePlayer + " Computer Score: " + scoreComputer;
                    done = true;
                    gameOver = true;
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            //State of the game right here.
            IsMouseVisible = true;
            //Position of the mouse
            var mouseState = Mouse.GetState();
            //Sound state
            soundState();
            //Hover state
            hoverState(mouseState);
            //Click State();
            clickState(mouseState);
            //Player mode
            if (isClickedPlayGame)
            {
                playGame(gameTime);
            }
            else if (isClickedDemoGame)
            {
                demoMode(gameTime);
            }


                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed  || (isClickExit && !isClickedDemoGame
                && !isClickedPlayGame))
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
            //Begin draw here
            spriteBatch.Begin();

            //Menu
            spriteBatch.Draw(menu, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.Draw(playMenu, new Rectangle(888, 570, 220, 100), Color.White);
            spriteBatch.Draw(computerMenu, new Rectangle(730, 651, 520, 100), Color.White);
            spriteBatch.Draw(creditMenu, new Rectangle(730, 730, 500, 100), Color.White);
            spriteBatch.Draw(exitMenu, new Rectangle(750, 830, 500, 70), Color.White);
                     
            //End menu
            //Hover
            //Noted this is hardcoded and need to find a way to make it fit in many devices
            if (isHoverPlayer)
            {
                spriteBatch.Draw(hoverPlay, new Rectangle(888, 570, 220, 100), Color.White);
            }
           
            else if (isHoverComputer)
            {
                spriteBatch.Draw(hoverDemo, new Rectangle(730, 651, 520, 100), Color.White);
            }
            else if (isHoverCredit)
            {
                spriteBatch.Draw(hoverCredit, new Rectangle(730, 730, 500, 100), Color.White);
            }
            else if (isHoverExit)
            {
                spriteBatch.Draw(hoverExit, new Rectangle(750, 830, 500, 70), Color.White);
            }
            //Click
            if (isClickedPlayGame)
            {
                //In the game
                spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
                ball.Draw(spriteBatch);
                human.Draw(spriteBatch);
                computer.Draw(spriteBatch);
            }
            else if(isClickedDemoGame){
                spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
                ball.Draw(spriteBatch);
                rightComputer.Draw(spriteBatch);
                computer.Draw(spriteBatch);
            }
            

            // Draw running score string
            if (isClickedPlayGame)
            {
                spriteBatch.DrawString(Font1, "Computer: " + scoreComputer, new Vector2(5, 10),
                Color.Yellow);
                spriteBatch.DrawString(Font1, "Player: " + scorePlayer,
                new Vector2(graphics.GraphicsDevice.Viewport.Width - Font1.MeasureString("Player: " +
                ball.scorePlayer).X - 30, 10), Color.Yellow);
            }
            else if (isClickedDemoGame)
            {
                spriteBatch.DrawString(Font1, "Left AI: " + scoreComputer, new Vector2(5, 10),
                Color.Yellow);
                spriteBatch.DrawString(Font1, "Right AI: " + scorePlayer,
                new Vector2(graphics.GraphicsDevice.Viewport.Width - Font1.MeasureString("Right AI: " +
                ball.scorePlayer).X - 30, 10), Color.Yellow);
            }
            //draw victory/consolation message
            if (done)
            {
                FontPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 300,
                (graphics.GraphicsDevice.Viewport.Height / 2) - 50);
                spriteBatch.DrawString(Font1, victory, FontPos, Color.Yellow);
            }

            //End draw here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
