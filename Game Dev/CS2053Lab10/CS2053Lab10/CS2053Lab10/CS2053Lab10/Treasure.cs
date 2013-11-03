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
    class Treasure
   {
        int screenWidth;
        int screenHeight;

        Texture2D texture;
        int width = 20;
        int height = 20;
        Rectangle myBox;
        Vector2 myVelocity;

        // Attack Speed
        float myMaxSpeed = 4f;

        //X pos and Y pos
        int x, y;

        public Treasure(int posX, int posY)
        {
            x = posX;
            y = posY;
            myBox = new Rectangle(x, y, width, height);
        }

        public void setTexture(Texture2D t) { texture = t; }

        public Rectangle getBox() { return myBox; }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, myBox, Color.White);
        }

    }
}
