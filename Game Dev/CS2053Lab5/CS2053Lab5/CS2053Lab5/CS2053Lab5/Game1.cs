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

namespace CS2053Lab5
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool gameWon = false;

        // Health
        int health = 1000;
        String score;
        SpriteFont Health;
        Vector2 healthLocation;

        
        // Set up for background
        Texture2D backgroundTexture;
        Rectangle backgroundRectangle;
        int screenWidth;
        int screenHeight;
        bool tooFar = false;


        // PlayerModel
        Model playerModel;
        Vector3 modelPosition = Vector3.Zero;
        Vector3 modelVelocity = Vector3.Zero;
        float modelRotation = 0.0f;
        float modelRotationVelocity = 0.01f;
        Vector3 cameraPosition = new Vector3(0.0f, 50.0f, 5000.0f);
        float aspectRatio;        // The aspect ratio determines how to scale 3d to 2d projection.

        // GameModel
        Model gameModel;
        Vector3 modelPositionG = Vector3.Zero;
        Vector3 modelVelocityG = Vector3.Zero;
        float modelRotationG = 0.0f;
        

        // Timer
        SpriteFont GameTimerFont;
        Vector2 gameTimerLocation;
        String gameTimer;
        double second;
        double miliSec;

        // Win / Lose pop up 
        SpriteFont WinLoseFont;
        Vector2 WinLoseLocation;
        String winLose;


        float vz = 10f;
        float vy = 10f;
        float vx = 10f;
        Random random = new Random();
       
 

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
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;
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

            // Load background
            backgroundTexture = this.Content.Load<Texture2D>("Sunset");
            backgroundRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            //Health
            Health = this.Content.Load<SpriteFont>("SpriteFont1");
            healthLocation = new Vector2(0, 0);



           // playerModel = Content.Load<Model>("Models\\p1_wedge");
            playerModel = Content.Load<Model>("Models\\p1_wedge");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;


            // gameModel
            gameModel = Content.Load<Model>("Models\\p1_wedge");

            // Timer
            GameTimerFont = this.Content.Load<SpriteFont>("SpriteFont2");
            gameTimerLocation = new Vector2(0, 25);

            // Win lose font
            WinLoseFont = this.Content.Load<SpriteFont>("SpriteFont3");
            WinLoseLocation = new Vector2(screenWidth / 2 - 50, screenHeight / 2);
         
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
            if (!gameWon)
            {

                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                // TODO: Add your update logic here

                miliSec = 1000 - gameTime.TotalGameTime.Milliseconds % 1000;
                second = 59 - gameTime.TotalGameTime.Seconds;

                gameTimer = "Time remaining: " + second + "." + (int)(miliSec / 100);


                if (modelPositionG.Y > 1500)
                { //x : -2000 to 2000
                    vy = -vy;   // LEFT OR RIGHT OF SCREEN

                }
                else if (modelPositionG.Y < -1500)
                    vy = -vy;

                modelPositionG.Y += vy;


                if (modelPositionG.Z > 1500)
                { //x : -2000 to 2000
                    vz = -vz;   // LEFT OR RIGHT OF SCREEN

                }
                else if (modelPositionG.Z < -1500)
                    vz = -vz;

                modelPositionG.Z += vz;

                if (modelPositionG.X > 2000)
                { //x : -2000 to 2000
                    vx = -vx;   // LEFT OR RIGHT OF SCREEN

                }
                else if (modelPositionG.X < -2000)
                    vx = -vx;

                modelPositionG.X += vx;


                // modelPositionG.Y += 10f;    // UP
                // modelPositionG.X += 10f;    // RIGHT


                if (Keyboard.GetState().IsKeyDown(Keys.Escape)) this.Exit();
                KeyboardState currentKeyState = Keyboard.GetState();

                // lOOK LEFT TO RIGHT
                if (currentKeyState.IsKeyDown(Keys.A)) modelRotation += modelRotationVelocity;
                if (currentKeyState.IsKeyDown(Keys.D)) modelRotation -= modelRotationVelocity;
                modelVelocity.X = (float)Math.Sin(modelRotation);
                modelVelocity.Z = (float)Math.Cos(modelRotation);


                modelVelocity *= 10f;

                // MOVE IN AND OUT
                if (currentKeyState.IsKeyDown(Keys.W) && modelPosition.Z > -1500 && modelPosition.X > -2000) modelPosition -= modelVelocity;
                if (currentKeyState.IsKeyDown(Keys.W) && modelPosition.Z < 1500 && modelPosition.X < 2000) modelPosition -= modelVelocity;
                if (currentKeyState.IsKeyDown(Keys.S) && modelPosition.Z < 1500) modelPosition += modelVelocity;

                // MOVE UP AND DOWN
                if (currentKeyState.IsKeyDown(Keys.Up) && modelPosition.Y > -1500) modelPosition.Y += 10f;
                if (currentKeyState.IsKeyDown(Keys.Down) && modelPosition.Y < 1500) modelPosition.Y -= 10f;

                // SLIDE LEFT TO RIGHT
                if (currentKeyState.IsKeyDown(Keys.Q) && modelPosition.X > -2000) modelPosition.X -= 10f;
                if (currentKeyState.IsKeyDown(Keys.E) && modelPosition.X < 2000) modelPosition.X += 10f;

                int dest = (int)Vector3.Distance(modelPosition, modelPositionG);

                if (dest > 2500 || dest < 1000)
                {
                    tooFar = true;
                    health--;
                }

                score = "Health: " + health;

                if (second == 0 && miliSec == 0)
                {
                    winLose = "YOU WIN!!!";
                    gameWon = true;
                }
                if (health == 0)
                {
                    winLose = "YOU LOSE!!!";
                    gameWon = true;
                }

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Draw the back ground
            spriteBatch.Begin();
            if (tooFar)
            {
                backgroundTexture = this.Content.Load<Texture2D>("background5");
                tooFar = false;
            }
            else
                backgroundTexture = this.Content.Load<Texture2D>("Sunset");

                spriteBatch.Draw(backgroundTexture, backgroundRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            spriteBatch.End();


            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[playerModel.Bones.Count];
            playerModel.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the player model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in playerModel.Meshes)
            {
                // This is where the mesh orientation is set, as well as camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] *
                        Matrix.CreateRotationY(modelRotation)
                        * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition,
                        Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();

 
            }

            // Copy any parent transforms.
            Matrix[] transformsG = new Matrix[gameModel.Bones.Count];
            gameModel.CopyAbsoluteBoneTransformsTo(transformsG);

            // Draw the game model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in gameModel.Meshes)
            {
                // This is where the mesh orientation is set, as well as camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transformsG[mesh.ParentBone.Index] *
                        Matrix.CreateRotationY(modelRotationG)
                        * Matrix.CreateTranslation(modelPositionG);
                    effect.View = Matrix.CreateLookAt(cameraPosition,
                        Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
                }
                // Draw the mesh, using the effects set above.

                // Health
                spriteBatch.Begin();
                //spriteBatch.DrawString(GameScoreFont, gameScore, gameScoreLocation, Color.Orange);
                spriteBatch.DrawString(Health, score, healthLocation, Color.Orange);
                spriteBatch.End();

                // Timer 
                spriteBatch.Begin();
                spriteBatch.DrawString(GameTimerFont, gameTimer, gameTimerLocation, Color.Orange);
                spriteBatch.End();

                // Game win / lose
                if (gameWon)
                {
                    spriteBatch.Begin();
                    spriteBatch.DrawString(WinLoseFont, winLose, WinLoseLocation, Color.Orange);
                    spriteBatch.End();
                }

                mesh.Draw();


            }


            base.Draw(gameTime);
        }
    }
}
