
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Dynamic;

namespace PingPong
{
    class clsSprite
    {
        public Texture2D texture { get; set; } // sprite texture, read-only property
        public Vector2 position { get; set; } // sprite position on screen
        public Vector2 size { get; set; } // sprite size in pixels
        public Vector2 velocity { get; set; } // sprite velocity

        private Vector2 screenSize { get; set; } // screen size
        public Vector2 center { get { return position + (size / 2); } } // sprite center
        public float radius { get { return size.X / 2; } } // sprite radius
        public int scorePlayer = 0;
        public int scoreComputer = 0;
        float dt = 0.1f;
        //Ball collide with player
        public int Player_Collide(paddle playerPaddle)
        {

            //Check for hit right paddle
            //Check for go from top to bottom
            //position.X + size.X + velocity.X>= playerPaddle.position.X && position.Y + size.Y >= playerPaddle.position.Y
              //  && position.Y <= playerPaddle.position.Y
            if (velocity.Y > 0 && position.X + size.X + velocity.X >= playerPaddle.position.X && this.center.Y >= playerPaddle.position.Y
                && this.center.Y <= playerPaddle.position.Y + playerPaddle.size.Y)
            {
                return 1;
            }

            //Check for go from bottom to top
           else if(velocity.Y < 0 && this.position.X + this.size.X + velocity.X >= playerPaddle.position.X && this.center.Y >= playerPaddle.position.Y
                && this.center.Y <= playerPaddle.position.Y + playerPaddle.size.Y )
            {

                return 2;
            }
                
            return 0;
        }
        //Ball collide with computer
        public int Computer_Collide(paddle computerPaddle)
        {

            //Check for hit right paddle
            //Check for go from top to bottom
            //position.X + size.X + velocity.X>= playerPaddle.position.X && position.Y + size.Y >= playerPaddle.position.Y
            //  && position.Y <= playerPaddle.position.Y
            if (velocity.Y > 0 && position.X + velocity.X <= computerPaddle.position.X + computerPaddle.size.X && this.center.Y >= computerPaddle.position.Y
                && this.center.Y <= computerPaddle.position.Y + computerPaddle.size.Y)
            {

                return 1;
            }

            //Check for go from bottom to top
            else if (velocity.Y < 0 && position.X + velocity.X <= computerPaddle.position.X + computerPaddle.size.X && this.center.Y >= computerPaddle.position.Y
                && this.center.Y <= computerPaddle.position.Y + computerPaddle.size.Y)
            {
                return 2;
            }
         
            return 0;
        }
        public void Move()
        {
            
            //Touch the right border
            if (this.position.X + size.X >= screenSize.X)
            {
                //Game reset
                this.position = new Vector2(screenSize.X / 2, screenSize.Y / 2);
                velocity = new Vector2(10, -10);

            }
            // checking bottom border
            if (this.position.Y + size.Y > screenSize.Y)
                velocity = new Vector2(velocity.X, -velocity.Y);
            //Touch the left border
            if (this.position.X  <= 0)
            {
                //Game rest

                this.position = new Vector2(screenSize.X / 2, screenSize.Y / 2);
                velocity = new Vector2(10, -10);
            }
            // checking top border
            if (position.Y + velocity.Y < 0)
                velocity = new Vector2(velocity.X, -velocity.Y);
            // since we adjusted the velocity, just add it to the current position
            //Adjust the ball based on acceleration 
            dt += 0.005f;
            Vector2 tempt = velocity;
            tempt.Y *= 0.45f;
            position += tempt;
            //*dt + new Vector2(0.2f * dt* dt/2, 0.2f * dt * dt / 2);
        }
        public clsSprite(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int ScreenWidth,
            int ScreenHeight)
        {
            texture = newTexture;
            position = newPosition;
            size = newSize;
            screenSize = new Vector2(ScreenWidth, ScreenHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position,Color.White);
        }
    }
}
