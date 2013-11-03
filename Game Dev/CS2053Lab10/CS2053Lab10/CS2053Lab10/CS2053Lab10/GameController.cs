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
    class GameController
    {
        PlayerObject player;
        GameAgent agent;

        public GameController(PlayerObject p, GameAgent a)
        {
            player = p; agent = a;
        }

        public void control()
        {
            Rectangle box1 = player.getBox();
            Rectangle box2 = agent.getBox();
            if (box1.Intersects(box2))
            {
                player.processCollision();
                agent.processCollision();
            }
        }

    } 
}
