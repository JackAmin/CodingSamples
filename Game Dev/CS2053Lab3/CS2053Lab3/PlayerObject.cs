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
    class PlayerObject
    {
        int screenWidth;
        int screenHeight;

        Texture2D texture;
        int width = 100;
        int height = 100;
        Rectangle box;
        Vector2 velocity;

        int health = 100;

        // Bullet object
        List<BulletObject> bulletObjectList = new List<BulletObject>();
        Texture2D bulletTexture;

      

        // Random number for jump key
        Random random = new Random();
        int randomNumber;

        Boolean fireKeyDown = false, jumpKey = false, autoRotate = false;

        float rotationAngle = 0.0f;
        float rotationVelocity = -0.01f;

        public PlayerObject(int w, int h) 
        { 
            screenWidth = w; screenHeight = h; 
            box = new Rectangle(screenWidth/2-width/2, screenHeight/2-height/2, width, height); 
        }

        public int getHealth() {return health; } 
         public void setHealth(int h) {health = h; }

        // Bullet methods
        public void setBulletTexture(Texture2D bt) { bulletTexture = bt; }
        public List<BulletObject> getBulletObjectList() { return bulletObjectList; }

        public void setTexture(Texture2D t) { texture = t; }

        public void setVelocity(Vector2 v) { velocity = v; }

        public Rectangle getBox() { return box; }

        public void update()
        {
           

            KeyboardState kstate = Keyboard.GetState();

            if (kstate.IsKeyUp(Keys.F) && fireKeyDown)
            {
                BulletObject bullet = new BulletObject();
                bullet.setTexture(bulletTexture);
                Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
                bullet.setLocation(new Vector2(box.X, box.Y));
                Vector2 bulletVelocity = new Vector2(10*(float) Math.Cos(rotationAngle), 10*(float) Math.Sin(rotationAngle));
                bullet.setVelocity(bulletVelocity);
                bulletObjectList.Add(bullet);
                fireKeyDown = false;
                health--;
            }
            if (kstate.IsKeyDown(Keys.F))
            {
                fireKeyDown = true;
                

            }
 
            List<BulletObject> removeList = new List<BulletObject>();
            foreach (BulletObject b in bulletObjectList)
            {
                Rectangle bulletBox = b.getBox();
                if (bulletBox.X < 0 || bulletBox.X > screenWidth || bulletBox.Y < 0 || bulletBox.Y > screenHeight)
                { removeList.Add(b); }
                else { b.update(); }
            }
            foreach (BulletObject b in removeList)
            { bulletObjectList.Remove(b); }
        
            // Auto Rotate
            if (autoRotate)
            {
                rotationAngle += rotationVelocity;
                rotationAngle = rotationAngle % (float)(2 * Math.PI);

                if (rotationAngle <= -1 * Math.PI || rotationAngle >= 0)
                {
                    rotationVelocity = -1 * rotationVelocity;
                }
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                autoRotate = true; 
            }
            if (kstate.IsKeyDown(Keys.S))
            {
                autoRotate = false;
            }


            if (autoRotate == false)
            {

                // To rotate to the left
                if (kstate.IsKeyDown(Keys.Left))
                {
                    // if (rotationAngle > 0.2)
                    {
                        rotationAngle += rotationVelocity;
                        rotationAngle = rotationAngle % (float)(2 * Math.PI);
                    }
                    //  if (box.X > 0)
                    //    box.X = box.X - (int)velocity.X;
                }

                // To rotate to the right
                if (kstate.IsKeyDown(Keys.Right))
                {
                    //  if (rotationAngle > 0.6)
                    {
                        rotationAngle -= rotationVelocity;
                        rotationAngle = rotationAngle % (float)(2 * Math.PI);
                    }
                    // if (box.X < screenWidth - width)
                    //     box.X = box.X + (int)velocity.X;
                }
            }

            if (kstate.IsKeyUp(Keys.J) && jumpKey)
            {
                randomNumber = random.Next(0, screenWidth);
                box.X = randomNumber;
                jumpKey = false;
            }

            if (kstate.IsKeyDown(Keys.J))
            {
                jumpKey = true;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(box.X, box.Y);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Vector2 scale = new Vector2((float)width / texture.Width, (float)height / texture.Height);
            float levelDepth = 0f;
            spriteBatch.Draw(texture, position, null, Color.White, rotationAngle, origin, scale, SpriteEffects.None, levelDepth);
            
            // Drawing the bullet
            foreach (BulletObject b in bulletObjectList) { b.draw(spriteBatch); }
        
        }
    }
}