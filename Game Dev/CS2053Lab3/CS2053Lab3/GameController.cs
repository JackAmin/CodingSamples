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
    class GameController
    {
        int screenWidth;
        int screenHeight;

        PlayerObject playerObject;
        List<GameObject> gameObjectList;
        GameInfoObject gameInfoObject;
        GameInfoObject score;

        Texture2D gameObjectTexture;
        Texture2D bombTexture;

        int count = 0;
       // Boolean spaceKeyDown = false;
        Random random = new Random();

        int collisionCount = 0;
        int health;

        public GameController(int sw, int sh, PlayerObject p, List<GameObject> g, GameInfoObject i, GameInfoObject s)
        {
            screenWidth = sw; screenHeight = sh; playerObject = p; gameObjectList = g; gameInfoObject = i; score = s;
            health = playerObject.getHealth();
        }
      

        public void setTexture(Texture2D t, Texture2D b) { gameObjectTexture = t; bombTexture = b; }

        public void control()
        {
            generateGameObject();

            collisionControl();

            gameInfoObject.setGameInfo("Collision count = " + collisionCount);


            health = playerObject.getHealth();
            score.setGameInfo("Health: " + health);


        }

        void generateGameObject()
        {
            KeyboardState kstate = Keyboard.GetState();
           // if (kstate.IsKeyUp(Keys.Space) && spaceKeyDown)
            while(count < 20)
            {
                GameObject g = new GameObject(screenWidth, screenHeight);
                g.setTexture(gameObjectTexture);
                g.setBombTexture(bombTexture);
                int x = (int)random.Next(0, screenWidth - 50);
                int y = (int)random.Next(0, screenHeight - 200);
                g.setLocation(new Vector2(x, y));
                float vx = random.Next(-2, 2);
                float vy = (int)random.Next(-2, 2);
                g.setVelocity(new Vector2(vx, vy));
                gameObjectList.Add(g);
               // spaceKeyDown = false;

                count++;

            }
          //  if (kstate.IsKeyDown(Keys.Space)) { spaceKeyDown = true; }
        }

        void collisionControl()
        {
            List<BulletObject> bulletObjectList = playerObject.getBulletObjectList();
            List<BulletObject> bulletObjectRemoveList = new List<BulletObject>();
            List<GameObject> gameObjectRemoveList = new List<GameObject>();

            foreach (GameObject g in gameObjectList)
            {
                List<Bomb> bombList = g.getBombList();
                List<Bomb> bombRemoveList = new List<Bomb>();

                foreach (Bomb bo in bombList)
                {
                    foreach (BulletObject bu in bulletObjectList)
                    {
                        if (bo.getBox().Intersects(bu.getBox()))
                        {
                            bombRemoveList.Add(bo);
                            bulletObjectRemoveList.Add(bu);
                        }
                    }

                    if (bo.getBox().Intersects(playerObject.getBox()))
                    {
                        bombRemoveList.Add(bo);
                         health = health - 10;
                         playerObject.setHealth(health);
                       
                    }
                }

                foreach (BulletObject bu in bulletObjectRemoveList)
                {
                    bulletObjectList.Remove(bu);
                }

                foreach (Bomb bomb in bombRemoveList)
                {
                    bombList.Remove(bomb);
                }
                    
             


                foreach (BulletObject b in bulletObjectList)
                {
                    Rectangle gameObjectBox = g.getBox();
                    Rectangle bulletObjectBox = b.getBox();
                    if (bulletObjectBox.Intersects(gameObjectBox))
                    {
                        gameObjectRemoveList.Add(g);
                        bulletObjectRemoveList.Add(b);
                        collisionCount++;
                        health = health + 5;
                        if (health > 100)
                            health = 100;
                        playerObject.setHealth(health);
                    }
                }
            }
            foreach (GameObject g in gameObjectRemoveList) { gameObjectList.Remove(g); }
            foreach (BulletObject g in bulletObjectRemoveList) { bulletObjectList.Remove(g); }
        }
    }
}
