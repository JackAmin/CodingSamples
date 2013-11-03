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
    class GameAgent
    {
        int screenWidth;
        int screenHeight;

        bool waiting = false;

        Texture2D texture;
        int width = 20;
        int height = 20;
        Rectangle myBox;
        Vector2 myVelocity;

        // Attack Speed
        float myMaxSpeed = 4f;

        // Normal Speed
        float normSpeed = 4f;

        // Box to gaurd
        Vector2 topL, topR, bottomL, bottomR;
        Vector2 conerPosition;

        // Treasure to gaurd
        Treasure egg;

        // Last Angle
        float lastAngle;

        Vector2 myNewPosition;

        int health = 10;

        Random random = new Random(3);

        PlayerObject player;

        String state;

        public GameAgent(int w, int h, PlayerObject p, Treasure t)
        {
            screenWidth = w; screenHeight = h;
            player = p;
            resetPosition();
            myVelocity = new Vector2(1f, 1f);
            egg = t;
            topL = (new Vector2(egg.getBox().Center.X - 150, egg.getBox().Center.Y - 100));
            topR = (new Vector2(egg.getBox().Center.X + 150, egg.getBox().Center.Y - 100));
            bottomL = (new Vector2(egg.getBox().Center.X - 150, egg.getBox().Center.Y + 100));
            bottomR = (new Vector2(egg.getBox().Center.X + 150, egg.getBox().Center.Y + 100));
            conerPosition = topL;

            state = "wander";
        }

        public float getAngle(Vector2 p1, Vector2 p2)
        {
            Vector2 delta = p2 - p1;
            float angle = (float)Math.Atan(delta.Y / Math.Abs(delta.X));
            if (delta.X < 0.0f)
            {
                angle = (float)Math.PI - angle;
            }
            if (delta.X == 0 && delta.X == 0)
            {
                angle = lastAngle;
            }
            lastAngle = angle;
            return angle;
        }

        public void setTexture(Texture2D t) { texture = t; }

        public void setVelocity(Vector2 v) { myVelocity = v; }

        public Rectangle getBox() { return myBox; }

        public int getHealth() { return health; }

        public void behavior()
        {
            if (state.Equals("wander"))
            {
                wander();
                if (seeEnemy()) state = "attack";
                if (waiting) state = "circle";
            }
            else if (state.Equals("attack"))
            {
                attack();
                if (health < 5) { state = "flee"; }
                if (noEnemy()) {state = "wander";}
            }
            else if (state.Equals("flee"))
            {
                flee();
                if (noEnemy()) { state = "wander"; }
            }
            else if (state.Equals("circle"))
            {
                circle();
            }
        }

        void wander()
        {
            // To gard the egg
            Vector2 toPoint = wonderPoint();

            Vector2 myPosition = new Vector2(myBox.X, myBox.Y);

            Vector2 myDesiredVelocity = Vector2.Normalize(toPoint - myPosition) * normSpeed;
            myNewPosition = myPosition + myDesiredVelocity - myVelocity;
            if (myNewPosition.X >= 0 && myNewPosition.X <= screenWidth - myBox.Width
                && myNewPosition.Y >= 0 && myNewPosition.Y <= screenHeight - myBox.Height)
            {
                myBox.X = (int)myNewPosition.X;
                myBox.Y = (int)myNewPosition.Y;
            }


            Double dist = getDist(myBox.Center, topL);
            Point p = new Point(myBox.X, myBox.Y);
            if (dist != 0.0)
            {
                dist = getDist(p, topR);
            }
            if (dist != 0.0)
            {
                dist = getDist(p, bottomL);
            }
            if (dist != 0.0)
            {
                dist = getDist(p, bottomR);
            }

            if (dist == 0.0)
            {
                waiting = true;
            }


               


        }

        void circle()
        {
            Vector2 myPosition = new Vector2(myBox.X, myBox.Y);
            if (seeEnemy()) state = "attack";
            else
            {
                if (getDist(myPosition, topL) == 0.0)
                {
                    if (seeEnemy()) state = "attack";
                    conerPosition = topR;
                }
                if (getDist(myPosition, topR) == 0.0)
                {
                    if (seeEnemy()) state = "attack";
                    conerPosition = bottomR;
                }
                if (getDist(myPosition, bottomR) == 0.0)
                {
                    if (seeEnemy()) state = "attack";
                    conerPosition = bottomL;
                }
                if (getDist(myPosition, bottomL) == 0.0)
                {
                    if (seeEnemy()) state = "attack";
                    conerPosition = topL;
                }
            }


            Vector2 myDesiredVelocity = Vector2.Normalize(conerPosition - myPosition) * normSpeed;
            myNewPosition = myPosition + myDesiredVelocity - myVelocity;
            if (myNewPosition.X >= 0 && myNewPosition.X <= screenWidth - myBox.Width
            && myNewPosition.Y >= 0 && myNewPosition.Y <= screenHeight - myBox.Height)
            {
                myBox.X = (int)myNewPosition.X;
                myBox.Y = (int)myNewPosition.Y;
            }

        }


        void attack()
        {
            Rectangle playerBox = player.getBox();
            Vector2 playerPosition = new Vector2(playerBox.X, playerBox.Y);
            Vector2 playerVelocity = player.getVelocity();
            Vector2 myPosition = new Vector2(myBox.X, myBox.Y);

            Vector2 toPlayer = playerPosition - myPosition;
            float playerSpeed = (float)Math.Sqrt(playerVelocity.X * playerVelocity.X + playerVelocity.Y * playerVelocity.Y);
            double lookAheadTime = toPlayer.Length() / (myMaxSpeed + playerSpeed);
            float newX = playerPosition.X + playerVelocity.X * (float)lookAheadTime;
            float newY = playerPosition.Y + playerVelocity.Y * (float)lookAheadTime;
            Vector2 predictedPlayerPosition = new Vector2(newX, newY);
            Vector2 myDesiredVelocity = Vector2.Normalize(playerPosition - myPosition) * myMaxSpeed;
            myNewPosition = myPosition + myDesiredVelocity - myVelocity;
            if (myNewPosition.X >= 0 && myNewPosition.X <= screenWidth - myBox.Width
                && myNewPosition.Y >= 0 && myNewPosition.Y <= screenHeight - myBox.Height)
            {
                myBox.X = (int)myNewPosition.X;
                myBox.Y = (int)myNewPosition.Y;
            }
        }

        void flee()
        {
            Rectangle playerBox = player.getBox();
            if (seeEnemy())
            {
                Vector2 myPosition = new Vector2(myBox.X, myBox.Y);
                Vector2 playerPosition = new Vector2(playerBox.X, playerBox.Y);
                Vector2 myDesiredVelocity = Vector2.Normalize(myPosition - playerPosition) * myMaxSpeed;
                myNewPosition = myPosition + (myDesiredVelocity - myVelocity);
                if (myNewPosition.X >= 0 && myNewPosition.X <= screenWidth - myBox.Width)
                {
                    myBox.X = (int)myNewPosition.X;
                }
                else if (myNewPosition.Y >= 0 && myNewPosition.Y <= screenHeight - myBox.Height)
                {
                    myBox.Y = (int)myNewPosition.Y;
                }
                else resetPosition();
            }
        }

        Boolean seeEnemy()
        {
            int seeDistance = 200;
            Rectangle playerBox = player.getBox();
            int distanceX = playerBox.X - myBox.X;
            int distanceY = playerBox.Y - myBox.Y;
            int distance = (int)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            return distance < seeDistance;
        }

        Boolean noEnemy()
        {
            return !seeEnemy();
        }

        public void processCollision()
        {
            resetPosition();
        }

        void resetPosition()
        {
            int x = random.Next(0, screenWidth - width);
            int y = random.Next(0, screenHeight - height);
            myBox = new Rectangle(x, y, width, height);
        }


        double getDist(Point p, Vector2 q)
        {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }

        double getDist(Vector2 p, Vector2 q)
        {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, myBox, Color.White);
            Vector2 postion = new Vector2(myBox.X, myBox.Y);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Rectangle box = new Rectangle((int)myBox.X, (int)myBox.Y, (int)width, (int)height);
            Vector2 scale = new Vector2((float)box.Width / texture.Width, (float)box.Height / texture.Height);
            spriteBatch.Draw(texture, postion, null, Color.White, getAngle(postion, myNewPosition), origin, scale, SpriteEffects.None, 0f);
        }

        Vector2 wonderPoint()
        {
            double distance = getDist(myBox.Center, topL);
            Vector2 location = topL;

            if (getDist(myBox.Center, topR) < distance)
            {
                distance = getDist(myBox.Center, topR);
                location = topR;
            }
            if (getDist(myBox.Center, bottomR) < distance)
            {
                distance = getDist(myBox.Center, bottomR);
                location = bottomR;
            }
            if (getDist(myBox.Center, bottomL) < distance)
            {
                distance = getDist(myBox.Center, bottomL);
                location = bottomL;
            }

            return location;
        }
    }
}