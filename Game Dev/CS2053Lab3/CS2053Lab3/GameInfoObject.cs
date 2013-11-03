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
    class GameInfoObject
    {
        int screenWidth;
        int screenHeight;

        Vector2 location;
        SpriteFont font;
        String gameInfo;
        double gameHour, gameMinute, gameSecond;

        public GameInfoObject(int w, int h, int x, int y) { screenWidth = w; screenHeight = h; location = new Vector2(x, y - 50); }

        public void setFont(SpriteFont t) { font = t; }

        public void setLocation(Vector2 l) { location = l; }

        public void setGameInfo(String info) { gameInfo = info; }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, gameInfo, location, Color.Orange);
        }
    }
}
