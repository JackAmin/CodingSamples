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

namespace CS2053Lab6
{
    class GameObject
    {
        public Model model;
        public Vector3 modelPosition;
        public Vector3 modelVelocity;

        Vector3 cameraPosition;
        float aspectRatio;

        public GameObject(Model m, Vector3 mp, Vector3 mv, Vector3 cp, float ar)
        {
            model = m; modelPosition = mp; modelVelocity = mv;
            cameraPosition = cp; aspectRatio = ar;
        }

        public void update()
        {
            modelPosition += modelVelocity;
        }

        public void draw()
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];

            model.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple meshes, so loop.

            foreach (ModelMesh mesh in model.Meshes)
            {
                // This is where the mesh orientation is set, as well as camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index]
                        * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition,
                        Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);

                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }
        }
    }
}