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
    class GameObject
    {
        int screenWidth;
        int screenHeight;

        Texture2D texture;
        Vector2 location;
        Rectangle box;
        Vector2 velocity;

        public GameObject(int w, int h) { screenWidth = w; screenHeight = h; }

        public void setTexture(Texture2D t) { texture = t; }

        public void setLocation(Vector2 l) { location = l; }

        public void setBox(Rectangle b) { box = b; }

        public Rectangle getBox() { return box; }

        public void setVelocity(Vector2 v) { velocity = v; }

        public Vector2 getLocation() { return location; }

        public Vector2 getVelocity() { return velocity; }

        public void update()
        {
            location = location + velocity;
            if (location.X < 0 || location.X > screenWidth - box.Width)
                velocity.X = -velocity.X;
            if (location.Y < 0 || location.Y > screenHeight - box.Height)
                velocity.Y = -velocity.Y;
            box.X = (int)location.X;
            box.Y = (int)location.Y;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, box, Color.White);
        }
    }
}