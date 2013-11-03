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

namespace CS2053Lab2
{
    class GameInfoObject
    {

        SpriteFont font;
        Vector2 location;
        String gameInfo;

        public GameInfoObject(SpriteFont f, Vector2 v) { font = f; location = v; }

        public void setGameInfo(String s) {gameInfo = s; }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, gameInfo, location, Color.Orange);
        }
    }
}