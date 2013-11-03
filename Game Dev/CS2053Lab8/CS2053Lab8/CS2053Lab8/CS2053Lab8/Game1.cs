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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 2.0f);
        float aspectRatio;

        GolfCourse golfCourse;
        Texture2D golfCourseTexture;

        GolfBall golfBall;

        float timeInterval = 200f; //millisecond
        float lastTime = 0;

        // camera variable positions
        float camX = 0.0f, camY = -1.0f, camZ = 2.0f;

        //Ball Pos
        float posX = 1.0f, posY = 0.0f, posZ = 0.0f;
            
        //Ball vol
        float volX = -0.025f, volY = 0.025f, volZ = 0.0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            cameraPosition = new Vector3(camX, camY, camZ);

            Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 normal = new Vector3(0.0f, 0.0f, 1.0f);
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            float height = 2.0f;
            float width = 1.5f;
            golfCourse = new GolfCourse(origin, normal, up, height, width);

            golfBall = new GolfBall();
            golfBall.setCamera(cameraPosition);
            golfBall.setAspect(aspectRatio);
            golfBall.setPosition(new Vector3(posX, posY, posZ));
            golfBall.setVelocity(new Vector3(volX, volY, volZ));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            golfCourseTexture = Content.Load<Texture2D>("grass");

            golfBall.setModel(Content.Load<Model>("golf_ball"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            
            

            float currentTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            if (currentTime - lastTime < timeInterval) return;
            lastTime = currentTime;

            // 4 glass walls baby


            if (golfBall.getPosition().X < golfCourse.getLeft())
            {
                //Ball Pos
                posX = golfCourse.getLeft(); 
                posY = 0.0f;
                posZ = 0.0f;

                //Ball vol
                volX = 0.0f;
                golfBall.setVelocity(new Vector3(volX, volY, volZ));
                golfBall.setPosition(new Vector3(posX, posY, posZ));
            }

            if (golfBall.getPosition().X > golfCourse.getRight())
            {
                //Ball Pos
                posX = golfCourse.getRight();
                posY = 0.0f;
                posZ = 0.0f;

                //Ball vol
                volX = 0.0f;
                golfBall.setVelocity(new Vector3(volX, volY, volZ));
                golfBall.setPosition(new Vector3(posX, posY, posZ));
            }

            if (golfBall.getPosition().Z < golfCourse.getBottom())
            {
                //Ball Pos
                posX = 0.0f;
                posY = 0.0f;
                posZ = golfCourse.getBottom();

                //Ball vol
                volX = 0.0f;
                golfBall.setVelocity(new Vector3(volX, volY, volZ));
                golfBall.setPosition(new Vector3(posX, posY, posZ));
            }
            if (golfBall.getPosition().Z > golfCourse.getTop())
            {
                //Ball Pos
                posX = 0.0f;
                posY = 0.0f;
                posZ = golfCourse.getBottom();

                //Ball vol
                volX = 0.0f;
                golfBall.setVelocity(new Vector3(volX, volY, volZ));
                golfBall.setPosition(new Vector3(posX, posY, posZ));
            }

              
            KeyboardState kstate = Keyboard.GetState();

            // TODO: Add your update logic here
            if (kstate.IsKeyDown(Keys.Up))
            {
                cameraPosition.Y -= 0.1f;
                golfBall.setCamera(cameraPosition);

            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                cameraPosition.Y += 0.1f;
                golfBall.setCamera(cameraPosition);
            }


            if (kstate.IsKeyDown(Keys.Right))
            {
                cameraPosition.X -= 0.1f;
                golfBall.setCamera(cameraPosition);

            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                cameraPosition.X += 0.1f;
                golfBall.setCamera(cameraPosition);
            }

            if (kstate.IsKeyDown(Keys.Up) && kstate.IsKeyDown(Keys.LeftControl))
            {
                cameraPosition.Z -= 0.1f;
                golfBall.setCamera(cameraPosition);

            }

            if (kstate.IsKeyDown(Keys.Down) && kstate.IsKeyDown(Keys.LeftControl))
            {
                cameraPosition.Z += 0.1f;
                golfBall.setCamera(cameraPosition);
            }

            if (kstate.IsKeyDown(Keys.D))
            {
                posX += 0.1f;
                golfBall.setPosition(new Vector3(posX, posY, posZ));
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                posX -= 0.1f;
                golfBall.setPosition(new Vector3(posX, posY, posZ));
            }

            if (kstate.IsKeyDown(Keys.W))
            {
                posY += 0.1f;
                golfBall.setPosition(new Vector3(posX, posY, posZ));
            }

            if (kstate.IsKeyDown(Keys.S))
            {
                posY -= 0.1f;
                golfBall.setPosition(new Vector3(posX, posY, posZ));
            }

            if (kstate.IsKeyDown(Keys.NumPad8))
            {
                volZ += 0.1f;
                golfBall.setVelocity(new Vector3(volX, volY, volZ));
            }

            if (kstate.IsKeyDown(Keys.NumPad2))
            {
                volZ -= 0.1f;
                golfBall.setVelocity(new Vector3(volX, volY, volZ));
            }

            if (kstate.IsKeyDown(Keys.NumPad4))
            {
                volX -= 0.1f; 
                golfBall.setVelocity(new Vector3(volX, volY, volZ));
            }

            if (kstate.IsKeyDown(Keys.NumPad6))
            {
                volX += 0.1f;
                golfBall.setVelocity(new Vector3(volX, volY, volZ));
            }



            if (kstate.IsKeyDown(Keys.Space))
            {
                spaceInit();
            }


    




            


            golfBall.update();

            base.Update(gameTime);
        }

        void spaceInit()
        {
            golfBall.setPosition(new Vector3(posX, posY, posZ));
            golfBall.setVelocity(new Vector3(volX, volY, volZ));
            golfBall.setCamera(cameraPosition);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            drawGolfCourse();
            golfBall.draw();

            base.Draw(gameTime);
        }

        void drawGolfCourse()
        {
            VertexDeclaration quadVertexDeclaration;
            Matrix View, Projection;

            View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 4.0f / 3.0f, 1, 500);

            BasicEffect quadEffect;
            quadEffect = new BasicEffect(graphics.GraphicsDevice);
            quadEffect.EnableDefaultLighting();
            quadEffect.World = Matrix.Identity;
            quadEffect.View = View;
            quadEffect.Projection = Projection;
            quadEffect.TextureEnabled = true;
            quadEffect.Texture = golfCourseTexture;

            // create a VertexDeclaration for the vertex type used to define the quad, which included position, normal and texture coordinates
            quadVertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                    new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                }
            );

            foreach (EffectPass pass in quadEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
                GraphicsDevice.DrawUserIndexedPrimitives
                    <VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,
                    // supply the primitive data, and specify two triangles to draw.
                    golfCourse.vertices, 0, 4,
                    golfCourse.indexes, 0, 2);
            }
        }
    }
}
