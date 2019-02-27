
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
      
        public bool Player_Collides(paddle otherSprite)
        {
            //Player collides
            if (this.position.X - otherSprite.position.X + 20f >= 0  && this.position.Y + 50f >= otherSprite.position.Y)
                return true;
            else
                return false;
        }
        public bool Computer_Collides(paddle otherSprite)
        {
            //Computer collides
            if (otherSprite.position.X - this.position.X + 30f>= 0 && this.position.Y + 50f >= otherSprite.position.Y)
                return true;
            else
                return false;
        
        }

        public void Move()
        {
            // if we´ll move out of the screen, invert velocity
            // checking right boundary
            if (position.X + size.X + velocity.X > screenSize.X)
            {
                //Game pause
                velocity = new Vector2(0, 0);
                this.position = new Vector2(500, 500);
                velocity = new Vector2(5, 5);
               
            }
            // checking bottom boundary
            if (position.Y + size.Y + velocity.Y > screenSize.Y)
                velocity = new Vector2(velocity.X, -velocity.Y);
            // checking left boundary
            if (position.X + velocity.X < 0)
            {
                //Game pause
                velocity = new Vector2(0, 0);
                this.position = new Vector2(500, 500);
                velocity = new Vector2(10, 10);
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
