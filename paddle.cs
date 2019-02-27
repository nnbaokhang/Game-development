using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nguyen_Khang_lab3
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





        public void Move(clsSprite ball)
        {
            //This is for computer player, computer should follow the ball
            // if we´ll move out of the screen, invert velocity

            // checking bottom boundary,   // checking top boundary
    
                if (this.position.Y - ball.position.Y >= 0 && this.position.Y >= 0)
                {
                    velocity *= -1;
                }
                else if (this.position.Y - ball.position.Y <= 0 && this.position.Y < screenSize.Y)
                {
                    velocity *= 1;
                }
                else if(this.position.Y <= 0)
                {
                    velocity *= 1;
                }
                else if(this.position.Y > screenSize.Y)
                {
                    velocity *= -1;
                }
           
              
            // since we adjusted the velocity, just add it to the current position
            position += velocity;
            
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

    