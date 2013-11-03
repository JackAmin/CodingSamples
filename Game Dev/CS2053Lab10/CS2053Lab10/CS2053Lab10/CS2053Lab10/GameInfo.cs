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
    class GameInfo
    {
        PlayerObject player;
        GameAgent agent;

        Vector2 location;
        SpriteFont font;

        public GameInfo(int screenHeight, PlayerObject p, GameAgent a)
        {
            location = new Vector2(0, screenHeight - 25);
            player = p; agent = a;
        }

        public void setFont(SpriteFont t) { font = t; }

        public void draw(SpriteBatch spriteBatch)
        {
            int playerHealth = player.getHealth();
            int agentHealth = agent.getHealth();
            String gameInfo = "Player Health = " + playerHealth + "    Agent Health = " + agentHealth;
            spriteBatch.DrawString(font, gameInfo, location, Color.Orange);
        }

    }
}
