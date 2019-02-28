
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Nguyen_Khang_lab3
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
      
        public int Player_Collides(paddle otherSprite)
        {
            //Player collides
            if (Vector2.Distance(this.center, otherSprite.center) < 100)
            {
                //Collide with the first top half
                if (this.center.Y + this.velocity.Y <= otherSprite.center.Y && this.position.Y +this.size.Y + this.velocity.Y  >= otherSprite.position.Y)
                {
                    Console.WriteLine("Hit upper half");
                    Console.WriteLine("{0},{1},{2}", this.center.Y, otherSprite.center.Y, otherSprite.center.Y + otherSprite.size.Y / 2);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
                return 0;
                
        }
        public int Computer_Collides(paddle otherSprite)
        {
            //Computer collides
            if (Vector2.Distance(this.center, otherSprite.center) < 50)
            {
                if (this.center.Y <= otherSprite.center.Y )
                {
                    
                    return 1;
                }
                else if (this.position.Y >= otherSprite.position.Y + otherSprite.size.Y / 2 && this.position.Y <= otherSprite.position.Y + 3 * otherSprite.size.Y / 4)
                {
                    
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else
                return 0;

        }

        public void Move()
        {
            // if we´ll move out of the screen, invert velocity
            // checking right boundary
            //Create random number for reset the ball
            Random random = new Random();
                
            if (this.position.X + size.X  >= screenSize.X)
            {
                //Game pause
                velocity = new Vector2(0, 0);
                this.position = new Vector2(screenSize.X/2, screenSize.Y / 2);
                velocity = new Vector2(15, 15);

            }
            // checking bottom boundary
            if (this.position.Y + size.Y  > screenSize.Y)
                velocity = new Vector2(velocity.X, -velocity.Y);
            // checking left boundary
            if (this.position.X + size.Y <= 0)
            {
                //Game pause
                velocity = new Vector2(0, 0);
                this.position = new Vector2(screenSize.X / 2, screenSize.Y / 2);
                velocity = new Vector2(15, 15);
            }
            // checking top boundary
            if (position.Y + velocity.Y < 0)
                velocity = new Vector2(velocity.X, -velocity.Y);
            // since we adjusted the velocity, just add it to the current position
            position += velocity;
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
