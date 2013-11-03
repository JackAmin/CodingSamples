#region File Description
//-----------------------------------------------------------------------------
// Tank.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SimpleAnimation
{
    /// <summary>
    /// Helper class for drawing a tank model with animated wheels and turret.
    /// </summary>
    public class Tank
    {
        #region Fields


        // The XNA framework Model object that we are going to display.
        Model tankModel;


        // Shortcut references to the bones that we are going to animate.
        // We could just look these up inside the Draw method, but it is more
        // efficient to do the lookups while loading and cache the results.
        ModelBone leftBackWheelBone;
        ModelBone rightBackWheelBone;
        ModelBone leftFrontWheelBone;
        ModelBone rightFrontWheelBone;
        ModelBone leftSteerBone;
        ModelBone rightSteerBone;
        ModelBone turretBone;
        ModelBone cannonBone;
        ModelBone hatchBone;


        // Store the original transform matrix for each animating bone.
        Matrix leftBackWheelTransform;
        Matrix rightBackWheelTransform;
        Matrix leftFrontWheelTransform;
        Matrix rightFrontWheelTransform;
        Matrix leftSteerTransform;
        Matrix rightSteerTransform;
        Matrix turretTransform;
        Matrix cannonTransform;
        Matrix hatchTransform;

        
        // Array holding all the bone transform matrices for the entire model.
        // We could just allocate this locally inside the Draw method, but it
        // is more efficient to reuse a single array, as this avoids creating
        // unnecessary garbage.
        Matrix[] boneTransforms;


        // Current animation positions.
        float wheelRotationValue;
        float steerRotationValue;
        float turretRotationValue;
        float cannonRotationValue;
        float hatchRotationValue;

        // the four points
        Vector3 topR = new Vector3(-1500.0f, 0.0f, 0.0f);
        Vector3 topL = new Vector3(-1500.0f, 0.0f, 1000.0f);
        Vector3 bottomL = new Vector3(0.0f, 0.0f, 1000.0f);
        Vector3 bottomR = new Vector3(0.0f, 0.0f, 0.0f);


        // Velocity and Position
        float vol = 50.0f;
        Vector3 position = Vector3.Zero;
        Vector3 velocity = new Vector3(0.0f, 0.0f, 1.0f);
        float rotVal = 11.0f; // back
        float toRot = 0.1f;
        //left 12.55
        //front 14.14
        //right 15.75

        public void setPosition(Vector3 p) { position = p; }
        public void setVelocity(Vector3 v) { velocity = v; }

        public Vector3 getPosition() { return position; }
        public Vector3 getVelocity() { return velocity; }

        public void setRotVal(float r) { rotVal = r; }
        public float getRotVal() { return rotVal; }
        

        #endregion

        #region Properties


        /// <summary>
        /// Gets or sets the wheel rotation amount.
        /// </summary>
        public float WheelRotation
        {
            get { return wheelRotationValue; }
            set { wheelRotationValue = value; }
        }


        /// <summary>
        /// Gets or sets the steering rotation amount.
        /// </summary>
        public float SteerRotation
        {
            get { return steerRotationValue; }
            set { steerRotationValue = value; }
        }


        /// <summary>
        /// Gets or sets the turret rotation amount.
        /// </summary>
        public float TurretRotation
        {
            get { return turretRotationValue; }
            set { turretRotationValue = value; }
        }


        /// <summary>
        /// Gets or sets the cannon rotation amount.
        /// </summary>
        public float CannonRotation
        {
            get { return cannonRotationValue; }
            set { cannonRotationValue = value; }
        }


        /// <summary>
        /// Gets or sets the entry hatch rotation amount.
        /// </summary>
        public float HatchRotation
        {
            get { return hatchRotationValue; }
            set { hatchRotationValue = value; }
        }


        #endregion


        /// <summary>
        /// Loads the tank model.
        /// </summary>
        public void Load(ContentManager content)
        {
            // Load the tank model from the ContentManager.
            tankModel = content.Load<Model>("tank");

            // Look up shortcut references to the bones we are going to animate.
            leftBackWheelBone = tankModel.Bones["l_back_wheel_geo"];
            rightBackWheelBone = tankModel.Bones["r_back_wheel_geo"];
            leftFrontWheelBone = tankModel.Bones["l_front_wheel_geo"];
            rightFrontWheelBone = tankModel.Bones["r_front_wheel_geo"];
            leftSteerBone = tankModel.Bones["l_steer_geo"];
            rightSteerBone = tankModel.Bones["r_steer_geo"];
            turretBone = tankModel.Bones["turret_geo"];
            cannonBone = tankModel.Bones["canon_geo"];
            hatchBone = tankModel.Bones["hatch_geo"];

            // Store the original transform matrix for each animating bone.
            leftBackWheelTransform = leftBackWheelBone.Transform;
            rightBackWheelTransform = rightBackWheelBone.Transform;
            leftFrontWheelTransform = leftFrontWheelBone.Transform;
            rightFrontWheelTransform = rightFrontWheelBone.Transform;
            leftSteerTransform = leftSteerBone.Transform;
            rightSteerTransform = rightSteerBone.Transform;
            turretTransform = turretBone.Transform;
            cannonTransform = cannonBone.Transform;
            hatchTransform = hatchBone.Transform;

            // Allocate the transform matrix array.
            boneTransforms = new Matrix[tankModel.Bones.Count];
        }


        /// <summary>
        /// Draws the tank model, using the current animation settings.
        /// </summary>
        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            // Set the world matrix as the root transform of the model.
            tankModel.Root.Transform = world;

            // Calculate matrices based on the current animation position.
            Matrix wheelRotation = Matrix.CreateRotationX(wheelRotationValue);
            Matrix steerRotation = Matrix.CreateRotationY(steerRotationValue);
            Matrix turretRotation = Matrix.CreateRotationY(turretRotationValue);
            Matrix cannonRotation = Matrix.CreateRotationX(cannonRotationValue);
            Matrix hatchRotation = Matrix.CreateRotationX(hatchRotationValue);

            // Apply matrices to the relevant bones.
            leftBackWheelBone.Transform = wheelRotation * leftBackWheelTransform;
            rightBackWheelBone.Transform = wheelRotation * rightBackWheelTransform;
            leftFrontWheelBone.Transform = wheelRotation * leftFrontWheelTransform;
            rightFrontWheelBone.Transform = wheelRotation * rightFrontWheelTransform;
            leftSteerBone.Transform = steerRotation * leftSteerTransform;
            rightSteerBone.Transform = steerRotation * rightSteerTransform;
            turretBone.Transform = turretRotation * turretTransform;
            cannonBone.Transform = cannonRotation * cannonTransform;
            hatchBone.Transform = hatchRotation * hatchTransform;

            // Look up combined bone matrices for the entire model.
            tankModel.CopyAbsoluteBoneTransformsTo(boneTransforms);

            // Draw the model.
            foreach (ModelMesh mesh in tankModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransforms[mesh.ParentBone.Index] 
                        * Matrix.CreateTranslation(position);

                    effect.View = view;
                    effect.Projection = projection;

                    effect.EnableDefaultLighting();
                }

                mesh.Draw();
            }
        }


        public void update()
        {
            //Vector3 topR = new Vector3(-2000.0f, 0.0f, 0.0f);
            //Vector3 topL = new Vector3(-2000.0f, 0.0f, 1000.0f);
            //Vector3 bottomL = new Vector3(0.0f, 0.0f, 1000.0f);
            //Vector3 bottomR = new Vector3(0.0f, 0.0f, 0.0f);
            //Vector3 velocity = new Vector3(0.0f, 0.0f, 1.0f);
            //float rotVal = 11.0f; // back
            ////left 12.55
            ////front 14.14
            ////right 15.75

            if (rotVal >= 11.0f && rotVal <= 12.55f)  // top  left
            {
                if (position != topR)
                {
                    velocity = new Vector3(vol, 0.0f, 0.0f);
                    position -= velocity;
                }
                else
                {
                    rotVal += toRot;
                }

            }
            else if (rotVal >= 12.55f && rotVal <= 14.14f)  // top right
            {
                if (position != topL)
                {
                    velocity = new Vector3(0.0f, 0.0f, vol);
                    position += velocity;
                }
                else
                {
                    rotVal += toRot;
                }

            }
            else if (rotVal >= 14.14f && rotVal <= 15.75f)  // bottom left
            {
                if (position != bottomL)
                {
                    velocity = new Vector3(vol, 0.0f, 0.0f);
                    position += velocity;
                }
                else
                {
                    rotVal += toRot;
                }

            }
            else if (rotVal >= 11.00f && rotVal <= 17.5f)   // bottom right
            {
                if (position != bottomR)
                {
                    velocity = new Vector3(0.0f, 0.0f, vol);
                    position -= velocity;
                }
                else
                {
                    rotVal += toRot;
                }

                if (rotVal > 17.3) { rotVal = 11.00f;} 
            }
                




            //position -= velocity;
            //if (position == topR)
            //{
            //    velocity.Z = 0.0f;

            //}
   
        }
    }
}
