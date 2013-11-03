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

namespace CS2053Lab3
{
    class Bomb
    {
        Texture2D texture;
        int width = 30;
        int height = 30;
        Rectangle box;
        Vector2 velocity;

        public void setTexture(Texture2D t) { texture = t; box = new Rectangle(0, 0, width, height); }

        public void setLocation(Vector2 l) { box.X = (int)l.X; box.Y = (int)l.Y; }

        public void setVelocity(Vector2 v) { velocity = v; }

        public Rectangle getBox() { return box; }

        public void update()
        {
            //box.X = box.X + (int)velocity.X;
            box.Y = box.Y + (int)velocity.Y;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            spriteBatch.Draw(texture, box, null, Color.White, 0f, origin, SpriteEffects.None, 0f);
        }
    }
}