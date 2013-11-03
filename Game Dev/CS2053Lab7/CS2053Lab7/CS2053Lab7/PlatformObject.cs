using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CS2053Lab7
{
    class PlatformObject
    {
        Vector2 position;
        Vector2 size;
        Texture2D texture;
        float angle;

        public void setTexture(Texture2D t) { texture = t; }
        public void setPosition(Vector2 p) { position = p; }
        public void setSize(Vector2 s) { size = s; }
        public Vector2 getPosition() { return position; }
        public Vector2 getSize() { return size; }
        public void setAngle(float a) { angle = a; }
        public float getAngle() { return angle; }

        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, null, Color.Orange,
                       angle, Vector2.Zero, size,
                       SpriteEffects.None, 0);
        }
    }
}
