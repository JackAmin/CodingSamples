using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CS2053Lab10
{
    class PlayerObject
    {
        int screenWidth;
        int screenHeight;

        Texture2D texture;
        int width = 20;
        int height = 20;
        Rectangle box;
        Vector2 velocity = Vector2.Zero;
	float speed = 3.0f;
        int health = 10;

        Random random = new Random(11);

        public PlayerObject(int w, int h)
        {
            screenWidth = w; screenHeight = h;
            resetPosition();
        }

        public void setTexture(Texture2D t) { texture = t; }

        public void setVelocity(Vector2 v) { velocity = v; }

        public Vector2 getVelocity() { return velocity; }

        public Rectangle getBox() { return box; }

        public void changeHealth(int change) { health += change; }

        public int getHealth() { return health; }

        public void behavior()
        {
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Left) && box.X > 0)
            {
                velocity.X = -speed;  
		box.X = box.X + (int)velocity.X;
            }
            if (kstate.IsKeyDown(Keys.Right) && box.X < screenWidth - width)
            {
                velocity.X = speed;
                box.X = box.X + (int)velocity.X;
            }
            if (kstate.IsKeyDown(Keys.Up) && box.Y > 0)
            {
                velocity.Y = -speed;
                box.Y = box.Y + (int)velocity.Y;
            }
            if (kstate.IsKeyDown(Keys.Down) && box.Y < screenHeight - height)
            {
                velocity.Y = speed;
                box.Y = box.Y + (int)velocity.Y;
            }
        }
 
        public void processCollision()
        {
            resetPosition();
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, box, Color.White);
        }

        void resetPosition()
        {
            int x = random.Next(0, screenWidth - width);
            int y = random.Next(0, screenHeight - height);
            box = new Rectangle(x, y, width, height);
        }
    }
}
