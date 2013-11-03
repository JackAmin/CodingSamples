using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CS2053Lab7
{
    class BallObject
    {
        Texture2D texture;
        Vector2 position;
        Vector2 size;
        Vector2 velocity;
        float angle = 0;

        public void setTexture(Texture2D t) { texture = t; }
        public void setPosition(Vector2 p) { position = p; }
        public void setSize(Vector2 s) { size = s; }
        public Vector2 getPosition() { return position; }
        public Vector2 getSize() { return size; }
        public void setAngle(float a) { angle = a; }
        public void setVelocity(Vector2 v) { velocity = v; }
        public Vector2 getVelocity() { return velocity; }


        public void update()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
            float rotationVelocity = (float)velocity.Length() * 0.1f;
            angle += rotationVelocity;
        }


        public void draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Rectangle box = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Vector2 scale = new Vector2((float)box.Width / texture.Width, (float)box.Height / texture.Height);
            spriteBatch.Draw(texture, position, null, Color.White, angle, origin, scale, SpriteEffects.None, 0f);
        }
    }
}
