using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CS2053Lab2
{
    class PlayerObject
    {
        int screenWidth;
        int screenHeight;

        Texture2D texture;
        Vector2 location;
        Rectangle box;
        Vector2 velocity;

        public PlayerObject(int w, int h) { screenWidth = w; screenHeight = h; }

        public void setTexture(Texture2D t) { texture = t; }

        public void setLocation(Vector2 l) { location = l; }

        public void setBox(Rectangle b) { box = b; }

        public void setVelocity(Vector2 v) { velocity = v; }

        public Rectangle getBox() { return box; }

        public Vector2 getLocation() { return location;  }

        public Vector2 getVelocity() { return velocity; }

        public void update(KeyboardState kstate)
        {
            if (kstate.IsKeyDown(Keys.Left))
            {
                if (location.X > 0)
                    location.X = location.X  - (int)velocity.X;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                if (location.X < screenWidth - 50)
                    location.X = location.X + (int)velocity.X;
            }
            if (kstate.IsKeyDown(Keys.Up))
            {
                if (location.Y > 0)
                    location.Y = location.Y - (int)velocity.Y;
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                if (location.Y < screenHeight - 50)
                    location.Y = location.Y + (int)velocity.Y;
            }
               box = (new Rectangle((int)location.X, (int)location.Y, 50, 50));

        }

       
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, box, Color.White);
        }
    }
}