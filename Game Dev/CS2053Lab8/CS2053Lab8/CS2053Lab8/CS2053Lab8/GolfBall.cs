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


namespace CS2053Lab8
{
    class GolfBall
    {
        Model model;
        Vector3 position = Vector3.Zero;
        Vector3 size;
        Vector3 velocity = Vector3.Zero;
        float rotationAngle = 0.0f;

        Vector3 cameraPosition;
        float aspectRatio;

        public void setModel(Model m) { model = m; }
        public void setPosition(Vector3 p) { position = p; }
        public void setSize(Vector3 s) { size = s; }
        public void setVelocity(Vector3 v) { velocity = v; }
        public void setCamera(Vector3 cp) { cameraPosition = cp; }
        public void setAspect(float ap) { aspectRatio = ap; }

        public Vector3 getPosition() { return position; }
        public Vector3 getSize() { return size; }
        public Vector3 getVelocity() { return velocity; }

        int timeCount = 0;

        float timeInterval = 100f; //millisecond
        float lastTime = 0;

        public void draw()
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index]
                        * Matrix.CreateScale(0.05f, 0.05f, 0.05f)
                        * Matrix.CreateRotationY(rotationAngle)
                        * Matrix.CreateTranslation(position);
                    effect.View = Matrix.CreateLookAt(cameraPosition,
                        Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
                }
                mesh.Draw();
            }
        }



        public void update()
        {
            timeCount++;
            if (timeCount >= 10)
            {
                velocity.Y += -0.000981f;
                if (position.Y <= 0.0f) velocity.Y = 0.0f;
            }
            position += velocity;
            float rotationVelocity = (float)velocity.Length() * 1.0f;
            rotationAngle += rotationVelocity;

        }
    }
}
