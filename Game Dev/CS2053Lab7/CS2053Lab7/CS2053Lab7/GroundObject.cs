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

namespace CS2053Lab7
{
    class GroundObject
    {
        Vector2 position;
        Vector2 size;
        Texture2D texture;

        public void setTexture(Texture2D t) { texture = t; }
        public void setPosition(Vector2 p) { position = p; }
        public void setSize(Vector2 s) { size = s; }
        public Vector2 getPosition() { return position; }
        public Vector2 getSize() { return size; }

        public void draw(SpriteBatch spriteBatch)
        {
            Rectangle box = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(texture, box, Color.White);
        }
    }
}