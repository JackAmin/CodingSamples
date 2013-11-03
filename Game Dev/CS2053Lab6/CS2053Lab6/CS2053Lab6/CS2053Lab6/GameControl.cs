using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS2053Lab6
{
    class GameControl
    {
        GameObject[] gameObjects;

        public GameControl(GameObject[] gos)
        {
            gameObjects = gos;
        }

        public void collisionControl()
        {
            for (int i = 0; i < gameObjects.Length - 1; i++)
                for (int j = 1; j < gameObjects.Length; j++)
                {
                    GameObject g1 = gameObjects[i];
                    GameObject g2 = gameObjects[j];
                    if (isCollision(g1, g2)) collisionResolution(g1, g2);
                }
        }

        Boolean isCollision(GameObject g1, GameObject g2)
        {
            for (int i = 0; i < g1.model.Meshes.Count; i++)
            {
                BoundingSphere g1BoundingSphere = g1.model.Meshes[i].BoundingSphere;
                g1BoundingSphere.Radius = 75f;
                
                g1BoundingSphere.Center += g1.modelPosition;
                for (int j = 0; j < g2.model.Meshes.Count; j++)
                {
                    BoundingSphere g2BoundingSphere = g2.model.Meshes[j].BoundingSphere;
                    g2BoundingSphere.Center += g2.modelPosition;
                    g2BoundingSphere.Radius = 75f;
                    if (g1BoundingSphere.Intersects(g2BoundingSphere))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        void collisionResolution(GameObject g1, GameObject g2)
        {
            g1.modelVelocity = -g1.modelVelocity;
            g2.modelVelocity = -g2.modelVelocity;
        }

    }
}
