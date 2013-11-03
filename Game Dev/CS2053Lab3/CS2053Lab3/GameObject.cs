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
    class GameObject
    {
        int screenWidth;
        int screenHeight;

        

        Texture2D texture;
        int with = 50;
        int height = 50;
        Rectangle box;
        Vector2 velocity;


        // Bomb object
        List<Bomb> bombList = new List<Bomb>();
        Texture2D bombTexture;

        // Random number for bombs key
        Random random = new Random();
        int randomNumber;

        public GameObject(int w, int h) { screenWidth = w; screenHeight = h - 200; box = new Rectangle(0, 0, with, height); }

        // Bullet methods
        public void setBombTexture(Texture2D bt) { bombTexture = bt; }
        public List<Bomb> getBombList() { return bombList; }

        public void setTexture(Texture2D t) { texture = t; }

        public void setLocation(Vector2 l) { box.X = (int) l.X; box.Y = (int) l.Y; }
 
        public void setVelocity(Vector2 v) { velocity = v; }

        public Rectangle getBox() { return box; }

        public void update()
        {
            box.X = box.X + (int) velocity.X;
            box.Y = box.Y + (int)velocity.Y;
            if (box.X < 0 || box.X > screenWidth - box.Width)
                velocity.X = -velocity.X;
            if (box.Y < 0 || box.Y > screenHeight - box.Height)
                velocity.Y = -velocity.Y;


            // Makin Da Bombs Drop
            randomNumber = random.Next(0, 100);
            if (randomNumber == 1)
            {
                Bomb bomb = new Bomb();
                bomb.setTexture(bombTexture);
                Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
                bomb.setLocation(new Vector2(box.X, box.Y));
                Vector2 bulletVelocity = new Vector2(1, 1);
                bomb.setVelocity(bulletVelocity);
                bombList.Add(bomb);
            }
     

            List<Bomb> removeList = new List<Bomb>();
            foreach (Bomb b in bombList)
            {
                Rectangle bombBox = b.getBox();
                if (bombBox.X < 0 || bombBox.X > screenWidth || bombBox.Y < 0 || bombBox.Y > screenHeight + 200)
                { removeList.Add(b); }
                else { b.update(); }
            }
            foreach (Bomb b in removeList)
            { bombList.Remove(b); }

        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, box, Color.White);

            // Drawing the bomb
            foreach (Bomb b in bombList) { b.draw(spriteBatch); }
        }
    }
}
