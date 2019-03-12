using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PingPong
{
    class paddle
    {
        public Texture2D texture { get; set; } // sprite texture, read-only property
        public Vector2 position { get; set; } // sprite position on screen
        public Vector2 size { get; set; } // sprite size in pixels
        public Vector2 velocity { get; set; } // sprite velocity
        private Vector2 screenSize { get; set; } // screen size
        public Vector2 center { get { return position + (size / 2); } } // sprite center
        public float radius { get { return size.X / 2; } } // sprite radius




        //AI working here
        public void Move(clsSprite ball)
        {
            //This is for computer player, computer should follow the ball
            //Computer should follow the ball Y coordinate

            // checking top boundary
            if (ball.position.X <= screenSize.X && ball.velocity.X <= 0)
            {
                Random getrandom = new Random();
                if (this.position.Y + this.velocity.Y <= 0)
                {
                    velocity = new Vector2(0, 8.5f);
                }
                //Checking bottom boundary
                else if (this.position.Y + this.size.Y / 1.17 + this.velocity.Y >= screenSize.Y)
                {
                    velocity = new Vector2(0, -8.5f);
                }
                //Keep track of the  the paddle Y is more than ball Y
                else if (this.center.Y - ball.center.Y >= getrandom.Next(1, 20))
                {
                    velocity = new Vector2(0, -8.5f);
                }
                //Keep track of the the paddle Y is less than ball Y
                else if (this.center.Y - ball.center.Y <= getrandom.Next(1, 20))
                {
                    velocity = new Vector2(0, 8.5f);
                }
                //If paddle Y == ball Y 
                else if (this.center.Y - ball.center.Y == getrandom.Next(1, 20))
                {
                    velocity = new Vector2(0, 8.5f);
                }

                // since we adjusted the velocity, just add it to the current position
                position += velocity;
            }
        }        public void MoveRight(clsSprite ball)
        {
            //This is for computer player, computer should follow the ball
            //Computer should follow the ball Y coordinate

            // checking top boundary
            if (ball.velocity.X >= 0)
            {
                Random getrandom = new Random();
                if (this.position.Y + this.velocity.Y <= 0)
                {
                    velocity = new Vector2(0, 8.5f);
                }
                //Checking bottom boundary
                else if (this.position.Y + this.size.Y / 1.17 + this.velocity.Y >= screenSize.Y)
                {
                    velocity = new Vector2(0, -8.5f);
                }
                //Keep track of the  the paddle Y is more than ball Y
                else if (this.center.Y - ball.center.Y >= getrandom.Next(1, 20))
                {
                    velocity = new Vector2(0, -8.5f);
                }
                //Keep track of the the paddle Y is less than ball Y
                else if (this.center.Y - ball.center.Y <= getrandom.Next(1, 20))
                {
                    velocity = new Vector2(0, 8.5f);
                }
                //If paddle Y == ball Y 
                else if (this.center.Y - ball.center.Y == getrandom.Next(1, 20))
                {
                    velocity = new Vector2(0, 8.5f);
                }

                // since we adjusted the velocity, just add it to the current position
                position += velocity;
            }
        }
        public paddle(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int ScreenWidth,
            int ScreenHeight)
        {
            texture = newTexture;
            position = newPosition;
            size = newSize;
            screenSize = new Vector2(ScreenWidth, ScreenHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position ,Color.White);
        }
    }
}

    